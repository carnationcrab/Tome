using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tome.API.Models
    {
        public class CharacterItem
        {
            [Key]
            [Column("id")]
            public Guid id { get; set; } = Guid.NewGuid();

            [Column("characterId")]
            public Guid characterId { get; set; }
            public Character character { get; set; } = null!;

            [Column("itemId")]
            public Guid itemId { get; set; }
            public Item item { get; set; } = null!;

            [Column("quantity")]
            public int quantity { get; set; } = 1;
        }

}
