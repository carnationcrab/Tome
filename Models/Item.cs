using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tome.API.Models.Tome.API.Models;

namespace Tome.API.Models
{
    public class Item
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("name")]
        public string name { get; set; }

        [Column("description")]
        public string? description { get; set; }

        [Column("createdAt")]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        [Column("updatedAt")]
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;

        // Items can belong to multiple universes
        public ICollection<ItemUniverse> itemUniverses { get; set; } = new List<ItemUniverse>();

        // Items can be owned by multiple characters
        public ICollection<CharacterItem>? characterItems { get; set; } = new List<CharacterItem>();

        // Items can have multiple modifiers
        public ICollection<ModifierAssignment>? ModifierAssignments { get; set; } = new List<ModifierAssignment>();
    }

}
