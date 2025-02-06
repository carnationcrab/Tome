namespace Tome.API.DTOs
{
    public class CharacterDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string attributes { get; set; }
    }
}
