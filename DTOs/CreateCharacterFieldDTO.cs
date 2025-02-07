public class CreateCharacterFieldDTO
{
    public Guid fieldId { get; set; } // Reference to existing Field
    public string value { get; set; }
    public string visibility { get; set; } = "private"; // "private", "universe", "public"
}
