using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tome.API.Models
{
    [Table("fields")] // Ensure correct mapping to DB table
    public class Field
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("name")]
        public string name { get; set; } = string.Empty;

        [Column("type")]
        public string type { get; set; } = "string"; // Example: string, int, boolean

        [Column("required")]
        public bool required { get; set; } = false;

        public ICollection<CharacterTypeField> characterTypeFields { get; set; } = new List<CharacterTypeField>();
        public ICollection<CharacterField> characterFields { get; set; } = new List<CharacterField>();

    }
}
