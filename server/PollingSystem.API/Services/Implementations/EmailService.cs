using PollingSystem.API.Models;
using PollingSystem.API.Services.Contracts;

namespace PollingSystem.API.Services.Implementations
{
    public class EmailService: IEmailService
    {
        public async Task<Result<string>> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                return await Task.FromResult(Result<string>.SuccessResult("Email sent successfully"));
            }
            catch (Exception ex)
            {
                return Result<string>.FailureResult(ex.Message);
            }
        }
    }
}
