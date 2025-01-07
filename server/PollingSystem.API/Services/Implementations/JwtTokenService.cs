using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using PollingSystem.API.Enums;
using PollingSystem.API.Services.Contracts;
using PollingSystem.API.Types;

namespace PollingSystem.Api.Services.Implementations
{
    public class JwtTokenService(IConfiguration configuration, ILogger<JwtTokenService> logger) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<JwtTokenService> _logger = logger;

        public string GenerateToken(int accountId, UserRole userRole)
        {
            var claims = new List<Claim>
            {
                new(JwtClaim.Id, accountId.ToString()),
                new(JwtClaim.Role, userRole.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(14),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsValidToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]!);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JWT:Issuer"],
                    ValidAudience = _configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromSeconds(5)
                }, out _);

                return true;
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogWarning("Token expired: {Message}", ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Invalid token: {Message}", ex.Message);
                return false;
            }
        }
    }
}
