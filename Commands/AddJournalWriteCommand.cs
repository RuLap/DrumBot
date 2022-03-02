using System;
using System.Threading.Tasks;
using DrumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DrumBot.Commands
{
    public class AddJournalWriteCommand : BaseCommand
    {
        public override string Name { get; }

        public AddJournalWriteCommand(IUserService userService, TelegramBot telegramBot)
        {
            _botClient = telegramBot.GetBot().Result;
            _userService = userService;
            Name = CommandNames.AddJournalWriteCommand;
        }
        
        public override async Task ExecuteAsync(Update update)
        {
            var user = _userService.GetOrCreate(update).Result;
            var message = "Введите номер страницы (нумерация с 5 в прикрепленном pdf) и номер рисунка через пробел, например: 1 15.";
            
            var inlineKeyboard = new ReplyKeyboardRemove();

            await _botClient.SendTextMessageAsync(user.ChatId, message, ParseMode.Markdown, replyMarkup:inlineKeyboard);
        }
    }
}