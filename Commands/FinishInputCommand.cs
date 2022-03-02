using System;
using System.Threading.Tasks;
using DrumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DrumBot.Commands
{
    public class FinishInputCommand : BaseCommand
    {
        private readonly IJournalService _journalService;
        
        public override string Name { get; }
        
        public FinishInputCommand(
            IUserService userService,
            IJournalService journalService,
            TelegramBot telegramBot)
        {
            _botClient = telegramBot.GetBot().Result;
            _userService = userService;
            _journalService = journalService;
            Name = CommandNames.FinishInputCommand;
        }
        
        public override async Task ExecuteAsync(Update update)
        {
            var user = _userService.GetOrCreate(update).Result;
            var message = "Данные успешно записаны.";

            if (int.TryParse(update.Message.Text, out int time))
            {
                var journalWrite = await _journalService.GetLastAdded(user);
                journalWrite.MinutesSpent = time;
                journalWrite.IsFinished = true;
                await _journalService.AddOrUpdateWrite(journalWrite);

                var inlineKeyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Добавить новую запись")
                    }
                });
                
                await _botClient.SendTextMessageAsync(user.ChatId, message, ParseMode.Markdown, replyMarkup: inlineKeyboard);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}