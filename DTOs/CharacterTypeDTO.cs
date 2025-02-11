namespace Tome.API.DTOs
{
    public class CharacterTypeDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string visibility { get; set; } // "private", "universe", "public"
        public List<FieldDTO> fields { get; set; } = new(); // Fields belonging to this type
    }
}
