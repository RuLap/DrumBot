using System.Threading.Tasks;
using DrumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DrumBot.Commands
{
    public class DownloadBookCommand : BaseCommand
    {
        public override string Name { get; }

        public DownloadBookCommand(IUserService userService, TelegramBot telegramBot)
        {
            _botClient = telegramBot.GetBot().Result;
            _userService = userService;
            Name = CommandNames.DownloadBookCommand;
        }
        
        public override async Task ExecuteAsync(Update update)
        {
            var link = @"https://disk.yandex.ru/i/zhDP-v07t8I4ww";
            var user = await _userService.GetOrCreate(update);
            
            await _botClient.SendTextMessageAsync(user.ChatId, link, ParseMode.Markdown);
        }
    }
}