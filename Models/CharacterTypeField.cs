using Tome.API.Models;

public class CharacterTypeField
{
    public Guid characterTypeId { get; set; }
    public CharacterType characterType { get; set; } = null!;

    public Guid fieldId { get; set; }
    public Field field { get; set; } = null!;
}
