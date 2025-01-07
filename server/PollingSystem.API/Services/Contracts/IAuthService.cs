using PollingSystem.API.Models;

namespace PollingSystem.API.Services.Contracts
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(UserForRegisterDto userForRegisterDto);
        Task<Result<string>> LoginAsync(UserForLoginDto userForLoginDto);
        Task<Result> ForgotPasswordAsync(UserForForgotPasswordDto userForForgotPasswordDto);
        Task<Result> ResetPasswordAsync(string token, string newPassword);
    }
}