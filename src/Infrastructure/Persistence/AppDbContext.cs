using Microsoft.EntityFrameworkCore;
using CampusLove.Domain.Entities;

namespace CampusLove.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<InteresUsuario> InteresesUsuario { get; set; } = null!;
        public DbSet<Interaccion> Interacciones { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(b =>
            {
                b.HasKey(u => u.UsuarioId);
                b.Property(u => u.Nombre).IsRequired().HasMaxLength(100);
                b.Property(u => u.Carrera).IsRequired().HasMaxLength(100);
                b.Property(u => u.Genero).IsRequired().HasMaxLength(20);
                b.Property(u => u.FrasePerfil).HasMaxLength(500);
                b.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired(false);
            });

            modelBuilder.Entity<InteresUsuario>(b =>
            {
                b.HasKey(i => i.InteresUsuarioId);
                b.Property(i => i.Interes).IsRequired().HasMaxLength(100);
                b.HasOne(i => i.Usuario).WithMany(u => u.Intereses).HasForeignKey(i => i.UsuarioId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Interaccion>(b =>
            {
                b.HasKey(x => x.InteraccionId);
                b.Property(x => x.TipoInteraccion).HasConversion<string>().HasMaxLength(10).IsRequired();
                b.HasIndex(x => new { x.UsuarioOrigenId, x.UsuarioDestinoId }).IsUnique();
                // relaciones solo para integridad referencial (sin navegación)
                b.HasOne<Usuario>().WithMany().HasForeignKey(x => x.UsuarioOrigenId).OnDelete(DeleteBehavior.Cascade);
                b.HasOne<Usuario>().WithMany().HasForeignKey(x => x.UsuarioDestinoId).OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
