public class CharacterDTO
{
    public Guid id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public Guid? characterTypeId { get; set; }

    public List<CharacterFieldDTO> fields { get; set; } = new();
}
