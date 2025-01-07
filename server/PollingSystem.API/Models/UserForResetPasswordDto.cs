using System.ComponentModel.DataAnnotations;

namespace PollingSystem.API.Models
{
    public class UserForResetPasswordDto
    {
        [Required]
        public required string Token { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 20 characters")]
        public required string NewPassword { get; set; }
    }
}