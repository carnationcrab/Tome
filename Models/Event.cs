namespace Tome.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public Guid UniverseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Universe Universe { get; set; }
    }

}
