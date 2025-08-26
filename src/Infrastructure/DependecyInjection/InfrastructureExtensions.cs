using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CampusLove.Infrastructure.Repositories;
using CampusLove.Domain.Ports;

namespace CampusLove.Infrastructure.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                // Repositorios
                services.AddScoped<IUsuarioRepository, EfUsuarioRepository>();
                services.AddScoped<IInteraccionRepository, EfInteraccionRepository>();
            });
        }
    }
}
