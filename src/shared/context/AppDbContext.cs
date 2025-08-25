using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CampusLove.src.shared.context;

    public class AppDbContext : DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    // DbSets para cada entidad
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas las configuraciones de la carpeta Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }        
}