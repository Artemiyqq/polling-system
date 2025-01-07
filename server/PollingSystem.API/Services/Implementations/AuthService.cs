using System.Web;
using PollingSystem.API.Models;
using PollingSystem.API.Services.Contracts;
using BCrypt.Net;
using PollingSystem.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace PollingSystem.API.Services.Implementations
{
    public class AuthService(IConfiguration configuration,
                             IEmailService emailService,
                             PollingDbContext pollingDbContext,
                             ITokenService tokenService) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly PollingDbContext _pollingDbContext = pollingDbContext;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<Result> RegisterAsync(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                User newUser = new()
                {
                    Username = userForRegisterDto.Username,
                    Email = userForRegisterDto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userForRegisterDto.Password)
                };

                await _pollingDbContext.Users.AddAsync(newUser);
                await _pollingDbContext.SaveChangesAsync();

                return Result.SuccessResult("Successfully registered. You can now Log In");
            }
            catch (Exception ex)
            {
                return Result.FailureResult(ex.Message);
            }
        }

        public async Task<Result<string>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            bool successfullyLoggedIn = await CheckLoginCredentials(userForLoginDto);

            if (!successfullyLoggedIn)
            {
                return Result<string>.FailureResult("Invalid credentials");
            }

            User user = await _pollingDbContext.Users.FirstAsync(u => u.Email == userForLoginDto.Email);

            string token = _tokenService.GenerateToken(user.Id, user.Role);

            return Result<string>.SuccessResult(token, "Successfully logged in");
        }

        public async Task<Result> ForgotPasswordAsync(UserForForgotPasswordDto userForForgotPasswordDto)
        {
            User? user = await _pollingDbContext.Users.FirstOrDefaultAsync(u => u.Email == userForForgotPasswordDto.Email);

            if (user is null)
            {
                return Result.FailureResult("User not found");
            }

            var token = await GeneratePasswordResetTokenAsync(user.Email);

            var callbackUrl = _configuration["ClientUrl"] + "/reset-password?token=" + HttpUtility.UrlEncode(token);

            await _emailService.SendEmailAsync(user.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            return Result.SuccessResult("Reset password link sent to your email");
        }

        public async Task<Result> ResetPasswordAsync(string token, string newPassword)
        {
            var hashedToken = HashToken(token);

            var user = await _pollingDbContext.Users.SingleOrDefaultAsync(u =>
                u.PasswordResetToken == hashedToken &&
                u.PasswordResetTokenExpires > DateTime.UtcNow);

            if (user == null)
            {
                return Result.FailureResult("Invalid or expired token");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpires = null;

            await _pollingDbContext.SaveChangesAsync();

            return Result.SuccessResult("Password reset successfully");
        }

        public async Task<bool> CheckLoginCredentials(UserForLoginDto userForLoginDto)
        {
            User? user = await _pollingDbContext.Users.FirstOrDefaultAsync(u => u.Email == userForLoginDto.Email);

            if (user == null)
            {
                return false;
            }

            bool correctPassword = BCrypt.Net.BCrypt.Verify(userForLoginDto.Password, user.PasswordHash);

            if (!correctPassword)
            {
                return false;
            }

            return true;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _pollingDbContext.Users.SingleOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found.");
            var token = GenerateResetToken();
            user.PasswordResetToken = HashToken(token);
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(1);

            await _pollingDbContext.SaveChangesAsync();

            return token;
        }

        public static string GenerateResetToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private static string HashToken(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }
    }
}