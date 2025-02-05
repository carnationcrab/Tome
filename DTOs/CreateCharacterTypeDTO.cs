namespace Tome.DTOs
{
    public class CreateCharacterTypeDTO
    {
        public string name { get; set; }
        public List<CreateFieldDTO>? fields { get; set; }
    }
}
