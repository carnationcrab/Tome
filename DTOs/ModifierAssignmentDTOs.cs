namespace Tome.API.DTOs
{
        public class ModifierAssignmentDTO
        {
            public Guid id { get; set; }
            public Guid modifierId { get; set; }
            public Guid? characterId { get; set; }
            public Guid? characterTypeId { get; set; }
            public Guid? itemId { get; set; }
            public Guid? fieldId { get; set; }
        }
    public class CreateModifierAssignmentDTO
    {
        public Guid modifierId { get; set; }
        public Guid? characterId { get; set; }
        public Guid? characterTypeId { get; set; }
        public Guid? itemId { get; set; }
        public Guid? fieldId { get; set; }
    }

}
