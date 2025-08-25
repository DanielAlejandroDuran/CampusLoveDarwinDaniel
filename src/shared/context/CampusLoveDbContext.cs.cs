using Microsoft.EntityFrameworkCore;
using CampusLoveDarwinDaniel.Modules.Users.Domain.Entities;
using CampusLoveDarwinDaniel.Modules.Interactions.Domain.Entities;
using CampusLoveDarwinDaniel.Modules.Matches.Domain.Entities;

namespace CampusLoveDarwinDaniel.Shared.Context
{
    public class CampusLoveDbContext : DbContext
    {
        public CampusLoveDbContext(DbContextOptions<CampusLoveDbContext> options)
            : base(options) { }

        // Tablas (DbSets)
        public DbSet<User> Users { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Age).IsRequired();
                entity.Property(u => u.Gender).IsRequired().HasMaxLength(20);
                entity.Property(u => u.Career).IsRequired().HasMaxLength(100);
                entity.Property(u => u.ProfilePhrase).HasMaxLength(500);
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(u => u.IsActive).HasDefaultValue(true);
                entity.Property(u => u.CreditsAvailable).HasDefaultValue(5);
                entity.Property(u => u.LastCreditRecharge).HasDefaultValueSql("CAST(GETDATE() AS DATE)");
            });

            // Interactions
            modelBuilder.Entity<Interaction>(entity =>
            {
                entity.HasKey(i => i.InteractionId);
                entity.Property(i => i.Type).IsRequired();
                entity.Property(i => i.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(i => i.IsActive).HasDefaultValue(true);

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(i => i.UserFromId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(i => i.UserToId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Matches
            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(m => m.MatchId);
                entity.Property(m => m.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(m => m.User1Id)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(m => m.User2Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
