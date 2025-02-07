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

        public ICollection<Field>? fields { get; set; }
        public ICollection<Character>? characters { get; set; }

        [Required]
        [Column("visibility")]
        public string visibility { get; set; } = "private"; // Default to private

        public List<Universe> universes { get; set; } = new List<Universe>();
    }
}
