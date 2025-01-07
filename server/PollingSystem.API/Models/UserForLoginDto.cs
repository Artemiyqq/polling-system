using System.ComponentModel.DataAnnotations;

namespace PollingSystem.API.Models
{
    public class UserForLoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "You must specify password between 4 and 20 characters")]
        public required string Password { get; set; }
        [Required]
        public required bool RememberMe { get; set; }
    }
}