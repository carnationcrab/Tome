﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tome.API.Models;

namespace Tome.API.Data
{
    public class TomeDbContext : IdentityDbContext<
        User,
        IdentityRole<Guid>,
        Guid,
        IdentityUserClaim<Guid>,
        IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
    {
        public TomeDbContext(DbContextOptions<TomeDbContext> options) : base(options) { }

        public DbSet<Universe> Universes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<CharacterType> CharacterTypes { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<UniverseCharacterType> UniverseCharacterTypes { get; set; }
        public DbSet<CharacterField> CharacterFields { get; set; }
        public DbSet<CharacterTypeField> CharacterTypeFields { get; set; }
        public DbSet<Modifier> Modifiers { get; set; }
        public DbSet<ModifierAssignment> ModifierAssignments { get; set; }
        public DbSet<Item> Items { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensures Identity tables are created
            
            // TODO All this horrible lowercase remapping is unhinged.
            // It needs to either die a firey death or be hidden in a fn
            // so I can forgive myself.


            // Tables (Lowercase Mapping)
            modelBuilder.Entity<Universe>().ToTable("universes");
            modelBuilder.Entity<Character>().ToTable("characters");
            modelBuilder.Entity<Event>().ToTable("events");
            modelBuilder.Entity<CharacterType>().ToTable("characterTypes");
            modelBuilder.Entity<Field>().ToTable("fields");
            modelBuilder.Entity<UniverseCharacterType>()
               .ToTable("universe_characterTypes")
               .HasKey(uct => new { uct.universeId, uct.characterTypeId });
            modelBuilder.Entity<CharacterTypeField>()
                .ToTable("characterTypeFields")
                .HasKey(ctf => new { ctf.characterTypeId, ctf.fieldId });
            modelBuilder.Entity<Modifier>().ToTable("modifiers");
            modelBuilder.Entity<ModifierAssignment>().ToTable("modifier_assignments");
            modelBuilder.Entity<Item>().ToTable("items");

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");

            // User (Lowercase Mapping)
            modelBuilder.Entity<User>().Property(u => u.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<User>().Property(u => u.UserName).HasColumnName("user_name");
            modelBuilder.Entity<User>().Property(u => u.NormalizedUserName).HasColumnName("normalized_user_name");
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.NormalizedEmail).HasColumnName("normalized_email");
            modelBuilder.Entity<User>().Property(u => u.EmailConfirmed).HasColumnName("email_confirmed");
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<User>().Property(u => u.SecurityStamp).HasColumnName("security_stamp");
            modelBuilder.Entity<User>().Property(u => u.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            modelBuilder.Entity<User>().Property(u => u.PhoneNumber).HasColumnName("phone_number");
            modelBuilder.Entity<User>().Property(u => u.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
            modelBuilder.Entity<User>().Property(u => u.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            modelBuilder.Entity<User>().Property(u => u.LockoutEnd).HasColumnName("lockout_end");
            modelBuilder.Entity<User>().Property(u => u.LockoutEnabled).HasColumnName("lockout_enabled");
            modelBuilder.Entity<User>().Property(u => u.AccessFailedCount).HasColumnName("access_failed_count");
            modelBuilder.Entity<User>().Property(u => u.createdAt).HasColumnName("created_at");

            // IdentityRole Table (Lowercase Mapping)
            modelBuilder.Entity<IdentityRole<Guid>>().Property(r => r.Id).HasColumnName("id");
            modelBuilder.Entity<IdentityRole<Guid>>().Property(r => r.Name).HasColumnName("name");
            modelBuilder.Entity<IdentityRole<Guid>>().Property(r => r.NormalizedName).HasColumnName("normalized_name");

            // Identity Relationships (Lowercase Mapping)
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserClaim<Guid>>().HasKey(uc => uc.Id);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasKey(rc => rc.Id);
            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

            // Identity Tables (Lowercase Mapping)
            modelBuilder.Entity<IdentityUserClaim<Guid>>().Property(uc => uc.Id).HasColumnName("id");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().Property(uc => uc.UserId).HasColumnName("user_id");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().Property(uc => uc.ClaimType).HasColumnName("claim_type");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().Property(uc => uc.ClaimValue).HasColumnName("claim_value");

            modelBuilder.Entity<IdentityUserLogin<Guid>>().Property(ul => ul.UserId).HasColumnName("user_id");
            modelBuilder.Entity<IdentityUserRole<Guid>>().Property(ur => ur.UserId).HasColumnName("user_id");
            modelBuilder.Entity<IdentityUserRole<Guid>>().Property(ur => ur.RoleId).HasColumnName("role_id");

            modelBuilder.Entity<IdentityUserToken<Guid>>().Property(ut => ut.UserId).HasColumnName("user_id");
            modelBuilder.Entity<IdentityUserToken<Guid>>().Property(ut => ut.LoginProvider).HasColumnName("login_provider");
            modelBuilder.Entity<IdentityUserToken<Guid>>().Property(ut => ut.Name).HasColumnName("name");

            // CharacterType <-> Field
            modelBuilder.Entity<CharacterTypeField>()
                .HasOne(ctf => ctf.characterType)
                .WithMany(ct => ct.characterTypeFields)
                .HasForeignKey(ctf => ctf.characterTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CharacterTypeField>()
                .HasOne(ctf => ctf.field)
                .WithMany(f => f.characterTypeFields)
                .HasForeignKey(ctf => ctf.fieldId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CharacterField>()
                .ToTable("characterFields")
                .HasKey(cf => cf.id);

            modelBuilder.Entity<CharacterField>()
                .HasOne(cf => cf.character)
                .WithMany(c => c.characterFields)
                .HasForeignKey(cf => cf.characterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CharacterField>()
                .HasOne(cf => cf.field)
                .WithMany(f => f.characterFields)
                .HasForeignKey(cf => cf.fieldId)
                .OnDelete(DeleteBehavior.Cascade);


            // Universe <-> CharacterTypes
            modelBuilder.Entity<UniverseCharacterType>()
                .HasOne(uct => uct.universe)
                .WithMany()
                .HasForeignKey(uct => uct.universeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UniverseCharacterType>()
                .HasOne(uct => uct.characterType)
                .WithMany()
                .HasForeignKey(uct => uct.characterTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Modifier <-> ModifierAssignment
            modelBuilder.Entity<ModifierAssignment>()
                .HasOne(ma => ma.modifier)
                .WithMany(m => m.assignments)
                .HasForeignKey(ma => ma.modifierId)
                .OnDelete(DeleteBehavior.Cascade);

            // Character <-> ModifierAssignment
            modelBuilder.Entity<ModifierAssignment>()
                .HasOne(ma => ma.character)
                .WithMany(c => c.ModifierAssignments)
                .HasForeignKey(ma => ma.characterId)
                .OnDelete(DeleteBehavior.Cascade);

            // CharacterType <-> ModifierAssignment
            modelBuilder.Entity<ModifierAssignment>()
                .HasOne(ma => ma.characterType)
                .WithMany(ct => ct.modifierAssignments)
                .HasForeignKey(ma => ma.characterTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Item <-> ModifierAssignment
            modelBuilder.Entity<ModifierAssignment>()
                .HasOne(ma => ma.item)
                .WithMany(i => i.ModifierAssignments)
                .HasForeignKey(ma => ma.itemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Field <-> ModifierAssignment
            modelBuilder.Entity<ModifierAssignment>()
                .HasOne(ma => ma.field)
                .WithMany(f => f.modifierAssignments)
                .HasForeignKey(ma => ma.fieldId)
                .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<Universe>()
            //    .HasMany(u => u.characterTypes)
            //    .WithMany(ct => ct.universes)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "universe_characterTypes",
            //        j => j.HasOne<CharacterType>().WithMany().HasForeignKey("character_type_id"),
            //        j => j.HasOne<Universe>().WithMany().HasForeignKey("universe_id")
            //    );

            // CharacterType -> Fields
            //modelBuilder.Entity<CharacterType>()
            //    .HasMany(ct => ct.fields)
            //    .WithOne(f => f.characterType)
            //    .HasForeignKey(f => f.characterTypeId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
