using System.Linq;
using System.Threading.Tasks;
using DrumBot.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using User = DrumBot.Entities.User;

namespace DrumBot.Services
{
    public class JournalService : IJournalService
    {
        private readonly DrumBotDBContext _context;

        public JournalService(DrumBotDBContext context)
        {
            _context = context;
        }
        
        public async Task AddOrUpdateWrite(JournalWrite journalWrite)
        {
            if (!_context.JournalWrites.Any(jw => jw.Id == journalWrite.Id))
            {
                await _context.JournalWrites.AddAsync(journalWrite);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<JournalWrite> GetLastAdded(User user)
        {
            return await _context.JournalWrites.Where(x => x.User.Id == user.Id).OrderByDescending(x => x.CreationDate).FirstAsync();
        }
    }
}