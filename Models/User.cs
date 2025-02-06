using Microsoft.AspNetCore.Identity;
using System;

namespace Tome.API.API.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
