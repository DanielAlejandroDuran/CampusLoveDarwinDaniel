using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CampusLove.Application.Services;
using CampusLove.Adapters.ConsoleApp;

namespace CampusLove.Application.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IHostBuilder AddApplication(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                // Servicios de aplicación
                services.AddScoped<MatchService>();

                // Adaptadores
                services.AddSingleton<ConsoleApp>();
            });
        }
    }
}