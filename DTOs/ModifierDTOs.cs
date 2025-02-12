namespace Tome.API.DTOs
{
    public class ModifierDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string operation { get; set; }

        public float value { get; set; } // Strength +2, HP +50
        public int? duration { get; set; } // Optional duration (turns, seconds)
        public string? condition { get; set; } // Conditional modifiers ("below 50% HP")
    }

    public class CreateModifierDTO
    {
        public string name { get; set; }
        public float value { get; set; }
        public string operation { get; set; }
        public int? duration { get; set; }
        public string? condition { get; set; }
    }

    public class UpdateModifierDTO
    {
        public string name { get; set; }
        public string operation { get; set; }

        public float value { get; set; }
        public int? duration { get; set; }
        public string? condition { get; set; }
    }

    public class AssignModifierDTO
    {
        public Guid modifierId { get; set; }
        public Guid? characterId { get; set; }
        public Guid? characterTypeId { get; set; }
        public Guid? itemId { get; set; }
        public Guid? fieldId { get; set; }
    }

}
