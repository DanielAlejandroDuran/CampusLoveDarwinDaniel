using Microsoft.Extensions.Logging;
using CampusLove.Domain.Ports;
using System.Threading.Tasks;

namespace CampusLove.Application.Services
{
    public class ApplicationStartupService : IApplicationStartupService
    {
        private readonly IDbHealthCheckService _dbHealthCheck;
        private readonly ILogger<ApplicationStartupService> _logger;

        public ApplicationStartupService(IDbHealthCheckService dbHealthCheck, ILogger<ApplicationStartupService> logger)
        {
            _dbHealthCheck = dbHealthCheck;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            _logger.LogInformation("Iniciando Campus Love...");
            
            // Verificar y preparar la base de datos
            await _dbHealthCheck.EnsureDatabaseCreatedAsync();
            
            var isHealthy = await _dbHealthCheck.IsHealthyAsync();
            if (!isHealthy)
            {
                _logger.LogError("La aplicación no puede iniciar: Base de datos no disponible");
                throw new System.InvalidOperationException("Base de datos no disponible");
            }
            
            _logger.LogInformation("Campus Love iniciado exitosamente");
        }
    }
}