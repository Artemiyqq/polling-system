namespace PollingSystem.API.Models
{
    public class Option
    {
        public int Id { get; set; }
        public required string OptionText { get; set; }
        public int PollId { get; set; }
        public required Poll Poll { get; set; }
        public ICollection<Vote> Votes { get; set; } = [];
    }
}