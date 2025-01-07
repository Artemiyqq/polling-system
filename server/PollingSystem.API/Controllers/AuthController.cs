using Microsoft.AspNetCore.Mvc;
using PollingSystem.API.Models;
using PollingSystem.API.Services.Contracts;

namespace PollingSystem.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            Result result = await _authService.RegisterAsync(userForRegisterDto);

            if (result.Success) return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPost("log-in")]
        public async Task<IActionResult> LogIn(UserForLoginDto userForLoginDto)
        {
            Result<string> result = await _authService.LoginAsync(userForLoginDto);

            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(UserForForgotPasswordDto userForForgotPasswordDto)
        {
            Result result = await _authService.ForgotPasswordAsync(userForForgotPasswordDto);

            if (result.Success)  return Ok(result.Message);
            return BadRequest(result.Message);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(UserForResetPasswordDto userForResetPasswordDto)
        {
            Result result = await _authService.ResetPasswordAsync(userForResetPasswordDto.Token, userForResetPasswordDto.NewPassword);

            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}