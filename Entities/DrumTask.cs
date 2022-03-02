using System.Collections.Generic;

namespace DrumBot.Entities
{
    public class DrumTask : BaseEntity
    {
        public int? Page { get; set; }
        
        public int? TaskNumber { get; set; }

        public List<JournalWrite> JournalWrites { get; set; }
    }
}