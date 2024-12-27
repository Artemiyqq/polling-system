using PollingSystem.API.Enums;

namespace PollingSystem.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public UserRole Role { get; set; } = UserRole.RegularUser;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Poll> Polls { get; set; } = [];
        public ICollection<Vote> Votes { get; set; } = [];
    }
}