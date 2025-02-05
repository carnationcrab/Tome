using System.Collections.Generic;
using System.Reflection.Emit;
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
            modelBuilder.Entity<Universe>()
                .HasMany(u => u.Characters)
                .WithOne(c => c.Universe)
                .HasForeignKey(c => c.UniverseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Universe>()
                .HasMany(u => u.Events)
                .WithOne(e => e.Universe)
                .HasForeignKey(e => e.UniverseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    }
