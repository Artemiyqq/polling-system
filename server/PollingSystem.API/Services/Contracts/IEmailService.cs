using PollingSystem.API.Models;

namespace PollingSystem.API.Services.Contracts
{
    public interface IEmailService
    {
        Task<Result<string>> SendEmailAsync(string email, string subject, string message);
    }
}