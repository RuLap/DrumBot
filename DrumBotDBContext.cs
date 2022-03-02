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
    }
}