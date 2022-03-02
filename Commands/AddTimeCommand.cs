using System;
using System.Threading.Tasks;
using DrumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DrumBot.Commands
{
    public class AddTimeCommand : BaseCommand
    {
        private readonly IJournalService _journalService;
        public override string Name { get; }

        public AddTimeCommand(
            IUserService userService,
            IJournalService journalService,
            TelegramBot telegramBot)
        {
            _botClient = telegramBot.GetBot().Result;
            _userService = userService;
            _journalService = journalService;
            Name = CommandNames.AddTimeCommand;
        }
        
        public override async Task ExecuteAsync(Update update)
        {
            var user = _userService.GetOrCreate(update).Result;
            var message = "Введите сколько минут вы делали это упражнение.";

            if (int.TryParse(update.Message.Text, out int bpm))
            {
                var journalWrite = await _journalService.GetLastAdded(user);
                journalWrite.MaxBpm = bpm;
                await _journalService.AddOrUpdateWrite(journalWrite);

                await _botClient.SendTextMessageAsync(user.ChatId, message, ParseMode.Markdown);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}