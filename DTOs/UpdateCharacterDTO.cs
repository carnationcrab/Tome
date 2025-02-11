namespace Tome.API.DTOs
{
    public class UpdateCharacterDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<UpdateCharacterFieldDTO>? customFields { get; set; } // Fields that may be modified
    }
}
