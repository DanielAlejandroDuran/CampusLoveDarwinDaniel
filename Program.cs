using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CampusLove.Infrastructure.Persistence;
using CampusLove.Infrastructure.Repositories;
using CampusLove.Domain.Ports;
using CampusLove.Application.Services;
using CampusLove.Adapters.ConsoleApp;
using System.Threading.Tasks;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((ctx, cfg) =>
    {
        cfg.AddJsonFile("appsettings.json", optional: true);
        cfg.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var connectionString = configuration.GetConnectionString("MySqlDB") ??
            Environment.GetEnvironmentVariable("CAMPUSLOVE_CONN") ??
            "server=localhost;port=3306;database=BaseProyecto;user=root;password=yourpassword";

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUsuarioRepository, CampusLove.Infrastructure.Repositories.EfUsuarioRepository>();
        services.AddScoped<IInteraccionRepository, CampusLove.Infrastructure.Repositories.EfInteraccionRepository>();
        services.AddScoped<MatchService>();

        services.AddSingleton<CampusLove.Adapters.ConsoleApp.ConsoleApp>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var app = scope.ServiceProvider.GetRequiredService<CampusLove.Adapters.ConsoleApp.ConsoleApp>();
    await app.RunAsync();
}
