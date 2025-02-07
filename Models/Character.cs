using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [Column("universe_id")]
        public Guid universeId { get; set; }
        public Universe universe { get; set; } = null!;

        public string description { get; set; }
        public string attributes { get; set; }  // JSONB equivalent, PostgreSQL

        [Column("characterTypeId")]
        public Guid? CharacterTypeId { get; set; } // Nullable in case the type is deleted

        public CharacterType? CharacterType { get; set; }

        public ICollection<FieldValue> fieldValues { get; set; }
    }
}
