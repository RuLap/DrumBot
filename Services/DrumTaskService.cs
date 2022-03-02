using System;
using System.Linq;
using System.Threading.Tasks;
using DrumBot.Entities;

namespace DrumBot.Services
{
    public class DrumTaskService : IDrumTaskService
    {
        private readonly DrumBotDBContext _context;

        public DrumTaskService(DrumBotDBContext context)
        {
            _context = context;
        }
        
        public async Task<DrumTask> AddOrGet(int page, int taskNumber)
        {
            DrumTask drumTask;
            try
            {
                drumTask = _context.DrumTasks.First(t => t.Page == page && t.TaskNumber == taskNumber);
                return drumTask;
            }
            catch(InvalidOperationException ex)
            {
                drumTask = new DrumTask
                {
                    Page = page,
                    TaskNumber = taskNumber
                };

                await _context.DrumTasks.AddAsync(drumTask);
                await _context.SaveChangesAsync();
            }

            return drumTask;
        }
    }
}