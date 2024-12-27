using Microsoft.EntityFrameworkCore;
using PollingSystem.API.Models;

namespace PollingSystem.Api.Data 
{
    public class PollingDbContext : DbContext
    {
        public PollingDbContext(DbContextOptions<PollingDbContext> options) : base(options)
        {
            Users = Set<User>();
            Polls = Set<Poll>();
            Options = Set<Option>();
            Votes = Set<Vote>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Poll>()
                .HasOne(p => p.Creator)
                .WithMany(u => u.Polls)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Poll>()
                .HasIndex(p => p.IsPublic);

            modelBuilder.Entity<Option>()
                .HasOne(o => o.Poll)
                .WithMany(p => p.Options)
                .HasForeignKey(o => o.PollId);
            
            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.UserId, v.PollId })
                .IsUnique();

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Poll)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.PollId);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Option)
                .WithMany(o => o.Votes)
                .HasForeignKey(v => v.OptionId);
        }
    }
}