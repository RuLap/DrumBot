using DrumBot.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrumBot
{
    public class DrumBotDBContext : DbContext
    {
        public DrumBotDBContext(DbContextOptions<DrumBotDBContext> options) : base(options)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<JournalWrite> JournalWrites { get; set; }
        public DbSet<DrumTask> DrumTasks { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JournalWrite>()
                .HasOne(p => p.DrumTask)
                .WithMany(p => p.JournalWrites);
        }
    }
}