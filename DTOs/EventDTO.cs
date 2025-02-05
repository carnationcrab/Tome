namespace Tome.DTOs
{
    public class EventDTO
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public Guid universeId { get; set; }
    }

}
