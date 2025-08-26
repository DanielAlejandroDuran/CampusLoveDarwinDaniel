using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CampusLove.Infrastructure.Persistence;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace CampusLove.Shared.Extensions
{
    public static class DatabaseExtensions
    {
        public static IHostBuilder AddDatabase(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
                
                // Configuración de conexión
                var connectionString = GetConnectionString(configuration);
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));

                services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(connectionString, serverVersion)
                           .EnableSensitiveDataLogging(false)
                );
            });
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString("MySqlDB") ??
                   Environment.GetEnvironmentVariable("CAMPUSLOVE_CONN") ??
                   "server=localhost;port=3306;database=BaseProyecto;user=root;password=yourpassword";
        }
    }
}