namespace Tome.DTOs
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Guid UniverseId { get; set; }
    }

}
