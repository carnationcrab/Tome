namespace Tome.API.DTOs
{
    public class CharacterFieldDTO
    {
        public Guid id { get; set; } // Field ID
        public string name { get; set; } // Field name
        public string value { get; set; } // Field value
        public bool isCustom { get; set; } // True if it's a custom attribute
        public string visibility { get; set; } // "private", "universe", "public"
    }
}
