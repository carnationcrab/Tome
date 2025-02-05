namespace Tome.Models
{
    public class Event
    {
        public Guid id { get; set; }
        public Guid universeId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }

        public Universe universe { get; set; }
    }

}
