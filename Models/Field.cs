﻿namespace Tome.Models
{
    public class Field
    {
        public Guid id { get; set; }
        public string name { get; set; }           // e.g., "Strength", "Dexterity"
        public string type { get; set; }           // e.g., "text", "number", "dropdown"

        public Guid? characterTypeId { get; set; }  // Foreign Key
        public CharacterType? characterType { get; set; }
    }
}
