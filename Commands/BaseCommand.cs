using System.Threading.Tasks;
using DrumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DrumBot.Commands
{
    public abstract class BaseCommand
    {
        protected TelegramBotClient _botClient;
        protected IUserService _userService;
        
        public abstract string Name { get; }
        
        public abstract Task ExecuteAsync(Update update);
    }
}