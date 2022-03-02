using System;
using System.Threading.Tasks;
using DrumBot.Entities;
using DrumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DrumBot.Commands
{
    public class AddTempoCommand : BaseCommand
    {
        private readonly IJournalService _journalService;
        private readonly IDrumTaskService _drumTaskService;
        
        public override string Name { get; }
        
        public AddTempoCommand(
            IUserService userService,
            IJournalService journalService,
            IDrumTaskService drumTaskService,
            TelegramBot telegramBot)
        {
            _botClient = telegramBot.GetBot().Result;
            _userService = userService;
            _journalService = journalService;
            _drumTaskService = drumTaskService;
            Name = CommandNames.AddTempoCommand;
        }
        
        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.GetOrCreate(update);
            var message = "Введите максимальный темп BPM, в котором вам удалось хорошо сыграть рисунок.";

            var values = update.Message.Text.Split(' ');

            if (values.Length != 2)
            {
                throw new Exception();
            }
            
            if (int.TryParse(values[0], out int page) && int.TryParse(values[1], out int lesson))
            {

                var drumTask = await _drumTaskService.AddOrGet(page, lesson);
                var journalWrite = new JournalWrite
                {
                    User = user,
                    DrumTask = drumTask
                };
                
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