using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tome.API.Models
{
    public class CharacterType
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; }

        [Required]
        [Column("name")]
        public string name { get; set; }

        //public Guid universeId { get; set; }
        //public Universe? universe { get; set; }

        public ICollection<CharacterTypeField> characterTypeFields { get; set; } = new List<CharacterTypeField>();

        // Many-to-One relationship with Characters
        public ICollection<Character> characters { get; set; } = new List<Character>();

        [Required]
        [Column("visibility")]
        public string visibility { get; set; } = "private"; // Default to private

    }
}
