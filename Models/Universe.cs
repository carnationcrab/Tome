using Microsoft.Extensions.Logging;

namespace Tome.Models
{
    public class Universe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Character> Characters { get; set; }
        public ICollection<Event> Events { get; set; }
    }

}
