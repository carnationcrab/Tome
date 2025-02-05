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
        public DbSet<CharacterType> CharacterTypes { get; set; }
        public DbSet<Field> Fields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Universe>().ToTable("universes");
            modelBuilder.Entity<Character>().ToTable("characters");
            modelBuilder.Entity<Event>().ToTable("events");
            modelBuilder.Entity<CharacterType>().ToTable("characterTypes");
            modelBuilder.Entity<Field>().ToTable("fields");

            // Universe -> CharacterTypes
            modelBuilder.Entity<Universe>()
                .HasMany(u => u.characterTypes)
                .WithOne(ct => ct.universe)
                .HasForeignKey(ct => ct.universeId)
                .OnDelete(DeleteBehavior.Cascade);

            // CharacterType -> Fields
            modelBuilder.Entity<CharacterType>()
                .HasMany(ct => ct.fields)
                .WithOne(f => f.characterType)
                .HasForeignKey(f => f.characterTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

