using System.Threading.Tasks;
using DrumBot.Entities;
using Telegram.Bot.Types;

namespace DrumBot.Services
{
    public interface IJournalService
    {
        Task<JournalWrite> AddWrite(Update update);
    }
}