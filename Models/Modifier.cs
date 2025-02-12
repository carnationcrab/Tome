namespace Tome.API.Models
{
    public class Modifier
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string name { get; set; }
        public float value { get; set; }
        public string operation { get; set; } // "add", "multiply", "override"
        public string? condition { get; set; } // Optional conditional rule
        public int? duration { get; set; } // Buff duration in seconds
        public ICollection<ModifierAssignment>? assignments { get; set; }
    }

}
