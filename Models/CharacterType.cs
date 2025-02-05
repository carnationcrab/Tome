using System;
using System.Collections.Generic;

namespace Tome.Models
{
    public class CharacterType
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public Guid universeId { get; set; }
        public Universe? universe { get; set; }

        public ICollection<Field>? fields { get; set; }
        public ICollection<Character>? characters { get; set; }
    }
}
