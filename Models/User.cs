using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tome.API.Models
{
    public class User : IdentityUser<Guid>
    {
        [Column("id")]
        public override Guid Id { get; set; } = Guid.NewGuid();
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
