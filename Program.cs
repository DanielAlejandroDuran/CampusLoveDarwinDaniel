using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CampusLove.Infrastructure.Persistence;
using CampusLove.Infrastructure.Repositories;
using CampusLove.Domain.Ports;
using CampusLove.Application.Services;
using CampusLove.Adapters.ConsoleApp;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((ctx, cfg) =>
    {
        cfg.AddJsonFile("appsettings.json", optional: true);
        cfg.AddEnvironmentVariables();
    })
    .ConfigureLogging((context, logging) =>
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
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var connectionString = configuration.GetConnectionString("MySqlDB") ??
            Environment.GetEnvironmentVariable("CAMPUSLOVE_CONN") ??
            "server=localhost;port=3306;database=BaseProyecto;user=root;password=yourpassword";

        // Si quieres evitar AutoDetect (evita abrir conexión ahora), especifica la versión:
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 33)); // ajusta si necesitas

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, serverVersion)
                   .EnableSensitiveDataLogging(false) // no muestres datos sensibles
        );

        services.AddScoped<IUsuarioRepository, EfUsuarioRepository>();
        services.AddScoped<IInteraccionRepository, EfInteraccionRepository>();
        services.AddScoped<MatchService>();

        services.AddSingleton<ConsoleApp>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var app = scope.ServiceProvider.GetRequiredService<ConsoleApp>();
    await app.RunAsync();
}
