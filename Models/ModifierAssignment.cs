namespace Tome.API.Models
{
    public class ModifierAssignment
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid modifierId { get; set; }
        public Modifier modifier { get; set; }

        public Guid? characterId { get; set; }
        public Character? character { get; set; }

        public Guid? characterTypeId { get; set; }
        public CharacterType? characterType { get; set; }

        public Guid? itemId { get; set; }
        public Item? item { get; set; }

        public Guid? fieldId { get; set; }
        public Field? field { get; set; }
    }

}
