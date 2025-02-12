namespace Tome.API.Models
{
    public class UniverseCharacterType
    {
        public Guid universeId { get; set; }
        public Universe universe { get; set; }

        public Guid characterTypeId { get; set; }
        public CharacterType characterType { get; set; }
    }
}
