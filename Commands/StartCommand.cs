using System.Threading.Tasks;
using DrumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DrumBot.Commands
{
    public class StartCommand : BaseCommand
    {
        private readonly IUserService _userService;

        public StartCommand(IUserService userService, TelegramBot telegramBot)
        {
            _userService = userService;
            _botClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.StartCommand;

        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.GetOrCreate(update);
            var inlineKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Начать заниматься"),
                    new KeyboardButton("Скачать книгу")
                }
            });
            await _botClient.SendTextMessageAsync(user.ChatId, "Добро пожаловать! На данный момент я могу фиксировать твой прогресс только по одной книге: \"George Lawrence Stone - Stick Control For The Snare Drummer\"", 
                ParseMode.Markdown, replyMarkup:inlineKeyboard);
        }
    }
}