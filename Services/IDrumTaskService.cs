using System.Threading.Tasks;
using DrumBot.Entities;

namespace DrumBot.Services
{
    public interface IDrumTaskService
    {
        Task<DrumTask> AddOrGet(int page, int taskNumber);
    }
}