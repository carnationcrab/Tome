namespace Tome.API.DTOs
{
    public class CreateCharacterDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public Guid universeId { get; set; }
        public Guid? characterTypeId { get; set; } // Can be null if character has no type
        public List<CreateCharacterFieldDTO>? customFields { get; set; } // Custom fields added during creation
    }
}
