namespace Tome.Models
{
    public class Character
    {
        public Guid id { get; set; }
        public Guid universeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string attributes { get; set; }  // JSONB equivalent, PostgreSQL

        public Universe universe { get; set; }
    }

}
