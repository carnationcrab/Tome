namespace Tome.API.DTOs
{
    public class ItemDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class CreateItemDTO
    {
        public string name { get; set; }
        public string description { get; set; }
    }

    public class UpdateItemDTO
    {
        public string name { get; set; }
        public string description { get; set; }
    }

}
