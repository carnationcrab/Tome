using Tome.API.Models;

public class CharacterField
{
    public Guid id { get; set; } = Guid.NewGuid();

    public Guid characterId { get; set; }
    public Character character { get; set; } = null!;

    public Guid fieldId { get; set; }
    public Field field { get; set; } = null!;

    public string value { get; set; } = string.Empty;
    public bool isCustom { get; set; } = false;

    public string visibility { get; set; } = "private";
}
