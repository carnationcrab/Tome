public class CharacterFieldDTO
{
    public Guid id { get; set; }
    public string name { get; set; } // Field Name
    public string value { get; set; }
    public bool isCustom { get; set; }
    public string visibility { get; set; } // "private", "universe", "public"
}
