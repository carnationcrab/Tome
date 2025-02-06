using System;
using System.Collections.Generic;

namespace Tome.API.API.Models
{
    public class Character
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public string description { get; set; }
        public string attributes { get; set; }  // JSONB equivalent, PostgreSQL

        public Guid universeId { get; set; }
        public Universe universe { get; set; }

        public Guid characterTypeId { get; set; }
        public CharacterType characterType { get; set; }

        public ICollection<FieldValue> fieldValues { get; set; }
    }
}
