namespace Tome.API.DTOs
{
    public class CreateCharacterFieldDTO
    {
        public Guid fieldId { get; set; } // Field being added
        public string value { get; set; } // Initial field value
        public string visibility { get; set; } // Visibility setting
    }
}
