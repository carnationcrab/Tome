namespace Tome.API.DTOs
{
    public class CreateFieldDTO
    {
        public string name { get; set; }
        public string type { get; set; } // e.g., "int", "string", "boolean"
        public bool required { get; set; }
    }
}
