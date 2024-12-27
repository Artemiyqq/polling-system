namespace PollingSystem.API.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int CreatorId { get; set; }
        public required User Creator { get; set; }
        public bool IsPublic { get; set; } = true;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public required ICollection<Option> Options { get; set; }
        public required ICollection<Vote> Votes { get; set; }
    }
}