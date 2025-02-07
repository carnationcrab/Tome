public class CreateCharacterDTO
{
    public string name { get; set; }
    public string description { get; set; }
    public Guid universeId { get; set; }
    public Guid? characterTypeId { get; set; }

    public List<CreateCharacterFieldDTO> customFields { get; set; } = new();
}
