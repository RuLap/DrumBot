using System.Threading.Tasks;
using Telegram.Bot.Types;
using User = DrumBot.Entities.User;

namespace DrumBot.Services
{
    public interface IUserService
    {
        Task<User> GetOrCreate(Update update);
    }
}