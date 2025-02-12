using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;;

namespace Tome.API.Models
{
    public class Character
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("name")]
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;

        [Required]
        [Column("universeId")]
        public Guid universeId { get; set; }
        public Universe universe { get; set; } = null!;
        [Column("characterTypeId")]
        public Guid? characterTypeId { get; set; } // Nullable in case the type is deleted

        public CharacterType? characterType { get; set; }

        // Characters can own multiple items
        public ICollection<CharacterItem>? characterItems { get; set; } = new List<CharacterItem>();

        // Characters can have modifiers applied
        public ICollection<ModifierAssignment>? ModifierAssignments { get; set; } = new List<ModifierAssignment>();

        // Characters can have custom fields
        public ICollection<CharacterField>? characterFields { get; set; } = new List<CharacterField>();
    }
}
