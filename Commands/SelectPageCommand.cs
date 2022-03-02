using System.Threading.Tasks;
using DrumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DrumBot.Commands
{
    public class SelectPageCommand : BaseCommand
    {
        public override string Name { get; }

        public SelectPageCommand(TelegramBot telegramBot)
        {
            _botClient = telegramBot.GetBot().Result;
            Name = CommandNames.SelectPageCommand;
        }
        
        public override async Task ExecuteAsync(Update update)
        {
            const string message = "Введите страницу, на которой вы выполняли упражнения.";

            await _botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, message, ParseMode.Markdown);
        }
    }
}