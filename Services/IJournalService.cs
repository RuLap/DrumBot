using System.Threading.Tasks;
using DrumBot.Entities;
using Telegram.Bot.Types;
using User = DrumBot.Entities.User;

namespace DrumBot.Services
{
    public interface IJournalService
    {
        Task AddOrUpdateWrite(JournalWrite journalWrite);

        Task<JournalWrite> GetLastAdded(User user);
    }
}