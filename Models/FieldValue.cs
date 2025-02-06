using System;

namespace Tome.API.Models
{
    public class FieldValue
    {
        public Guid id { get; set; }
        public Guid characterId { get; set; }
        public Character character { get; set; }

        public Guid fieldId { get; set; }
        public Field field { get; set; }

        public string value { get; set; }
    }
}
