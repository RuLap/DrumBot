using System;

namespace DrumBot.Entities
{
    public class JournalWrite : BaseEntity
    {
        public int MaxBpm { get; set; }
        
        public int MinutesSpent { get; set; }
        
        public bool IsFinished { get; set; }

        public DrumTask DrumTask { get; set; }

        public User User { get; set; }
    }
}