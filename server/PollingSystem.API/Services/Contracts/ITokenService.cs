using PollingSystem.API.Enums;

namespace PollingSystem.API.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(int accountId, UserRole userRole);
        bool IsValidToken(string token);
    }
}