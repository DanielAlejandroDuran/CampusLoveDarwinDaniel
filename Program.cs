using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CampusLove.Adapters.ConsoleApp;
using CampusLove.Shared.Extensions;
using CampusLove.Infrastructure.DependencyInjection;
using CampusLove.Application.DependencyInjection;

var host = Host.CreateDefaultBuilder(args)
    .AddCustomLogging()
    .AddDatabase() 
    .AddInfrastructure()
    .AddApplication()
    .Build();

using var scope = host.Services.CreateScope();

var app = scope.ServiceProvider.GetRequiredService<ConsoleApp>();
await app.RunAsync();