using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using User = DrumBot.Entities.User;

namespace DrumBot.Services
{
    public class UserService : IUserService
    {
        private readonly DrumBotDBContext _context;

        public UserService(DrumBotDBContext context)
        {
            _context = context;
        }

        public async Task<User> GetOrCreate(Update update)
        {
            var newUser = update.Type switch
            {
                UpdateType.CallbackQuery => new User
                {
                    Username = update.CallbackQuery.From.Username,
                    ChatId = update.CallbackQuery.Message.Chat.Id,
                    FirstName = update.CallbackQuery.Message.From.FirstName,
                    LastName = update.CallbackQuery.Message.From.LastName
                },
                UpdateType.Message => new User
                {
                    Username = update.Message.Chat.Username,
                    ChatId = update.Message.Chat.Id,
                    FirstName = update.Message.Chat.FirstName,
                    LastName = update.Message.Chat.LastName
                }
            };

            var user = await _context.Users.FirstOrDefaultAsync(x => x.ChatId == newUser.ChatId);

            if (user != null) return user;

            var result = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}