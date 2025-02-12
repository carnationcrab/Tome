namespace Tome.API.DTOs
{
    public class CreateCharacterTypeDTO
    {
        public string name { get; set; }
        public string visibility { get; set; } = "private"; // "private", "universe", "public"
        public List<Guid> fieldIds { get; set; } = new(); // Fields associated with this type
    }
}