using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tome.API.Models
{
    public class ItemUniverse
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; } = Guid.NewGuid();

        [Column("itemId")]
        public Guid itemId { get; set; }
        public Item item { get; set; } = null!;

        [Column("universeId")]
        public Guid universeId { get; set; }
        public Universe universe { get; set; } = null!;
    }
}
