public class AddCustomFieldDTO
{
    public Guid? fieldId { get; set; } // Optional: Use existing field if provided
    public string? name { get; set; } // Optional: Create a new field if no fieldId
    public string? type { get; set; } // Required if creating a new field
    public string value { get; set; } // JSON-style storage for flexibility
    public bool isCustom { get; set; } = true;
    public string visibility { get; set; } = "private"; // "private", "universe", "public"
}
