using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CampusLove.Domain.Ports;
using CampusLove.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace CampusLove.Infrastructure.Services
{
    public class DbHealthCheckService : IDbHealthCheckService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DbHealthCheckService> _logger;

        public DbHealthCheckService(AppDbContext context, ILogger<DbHealthCheckService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> IsHealthyAsync()
        {
            try
            {
                await _context.Database.CanConnectAsync();
                _logger.LogInformation("Conexión a la base de datos exitosa");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "No se pudo conectar a la base de datos");
                return false;
            }
        }

        public async Task EnsureDatabaseCreatedAsync()
        {
            try
            {
                await _context.Database.EnsureCreatedAsync();
                _logger.LogInformation("Base de datos verificada/creada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear/verificar la base de datos");
                throw;
            }
        }
    }
}