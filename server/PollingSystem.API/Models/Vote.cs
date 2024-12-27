namespace PollingSystem.API.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public required Poll Poll { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public int OptionId { get; set; }
        public required Option Option { get; set; }
        public DateTime VotedAt { get; set; } = DateTime.UtcNow;
    }
}