﻿namespace Tome.API.DTOs
{
    public class FieldDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
    }
}
