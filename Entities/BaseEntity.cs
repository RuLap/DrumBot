using System;
using Newtonsoft.Json;

namespace DrumBot.Entities
{
    public class BaseEntity
    {
        [JsonIgnore]
        public long Id { get; set; }
        
        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}