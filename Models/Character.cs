namespace Tome.Models
{
    public class Character
    {
        public Guid Id { get; set; }
        public Guid UniverseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Attributes { get; set; }  // JSONB equivalent, PostgreSQL

        public Universe Universe { get; set; }
    }

}
