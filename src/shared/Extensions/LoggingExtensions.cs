using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CampusLove.Shared.Extensions
{
    public static class LoggingExtensions
    {
        public static IHostBuilder AddCustomLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole();

                // Nivel mínimo global (opcional)
                logging.SetMinimumLevel(LogLevel.Warning);

                // Filtrar paquetes específicos: bajar ruido de EF Core (solo warnings+)
                logging.AddFilter("Microsoft", LogLevel.Warning);
                logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                logging.AddFilter("System", LogLevel.Warning);
            });
        }
    }
}