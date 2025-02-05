namespace Tome.DTOs
{
    public class CharacterTypeDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public List<FieldDTO> fields { get; set; }
    }
}
