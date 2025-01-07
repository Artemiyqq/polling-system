using System.ComponentModel.DataAnnotations;

namespace PollingSystem.API.Models
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "You must specify username between 4 and 50 characters")]
        public required string Username { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "You must specify password between 4 and 20 characters")]
        public required string Password { get; set; }
    }
}