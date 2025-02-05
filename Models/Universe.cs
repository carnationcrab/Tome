using Microsoft.Extensions.Logging;

namespace Tome.Models
{
    public class Universe
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Character> characters { get; set; }
        public ICollection<Event> events { get; set; }
    }

}
