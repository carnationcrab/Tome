using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tome.API.Models
{
    public class CharacterTypeField
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();

        [ForeignKey("CharacterType")]
        public Guid characterTypeId { get; set; }
        public CharacterType characterType { get; set; } = null!;

        [ForeignKey("Field")]
        public Guid fieldId { get; set; }
        public Field field { get; set; } = null!;
    }
}
