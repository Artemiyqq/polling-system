using System.ComponentModel.DataAnnotations;

namespace PollingSystem.API.Models
{
    public class UserForForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}