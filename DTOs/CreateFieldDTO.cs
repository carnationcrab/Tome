namespace Tome.API.DTOs
{
    public class CreateFieldDTO
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
    }
}
