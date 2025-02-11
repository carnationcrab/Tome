namespace Tome.API.DTOs
{
    public class FieldDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string type { get; set; } // Field type (e.g., string, int, etc.)
        public bool required { get; set; }
    }
}
