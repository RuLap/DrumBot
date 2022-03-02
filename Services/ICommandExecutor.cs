using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DrumBot.Services
{
    public interface ICommandExecutor
    {
        Task Execute(Update update);
    }
}