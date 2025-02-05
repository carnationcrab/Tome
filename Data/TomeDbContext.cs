using Microsoft.EntityFrameworkCore;
using Tome.Models;

namespace Tome.Data
{
    public class TomeDbContext : DbContext
    {
        public TomeDbContext(DbContextOptions<TomeDbContext> options) : base(options) { }

        public DbSet<Universe> Universes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map to lowercase table names
            modelBuilder.Entity<Universe>().ToTable("universes");
            modelBuilder.Entity<Character>().ToTable("characters");
            modelBuilder.Entity<Event>().ToTable("events");

            // Relationships
            modelBuilder.Entity<Universe>()
                .HasMany(u => u.characters)
                .WithOne(c => c.universe)
                .HasForeignKey(c => c.universeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Universe>()
                .HasMany(u => u.events)
                .WithOne(e => e.universe)
                .HasForeignKey(e => e.universeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
