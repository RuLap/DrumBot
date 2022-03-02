using System.Threading.Tasks;
using DrumBot.Entities;
using Telegram.Bot.Types;

namespace DrumBot.Services
{
    public class JournalService : IJournalService
    {
        public Task<JournalWrite> AddWrite(Update update)
        {
            throw new System.NotImplementedException();
        }
    }
}