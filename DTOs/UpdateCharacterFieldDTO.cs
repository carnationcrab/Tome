namespace Tome.API.DTOs
{
    public class UpdateCharacterFieldDTO
    {
        public Guid fieldId { get; set; } // Field ID to update
        public string value { get; set; } // Updated field value
        public string visibility { get; set; } // Updated visibility setting
    }
}
