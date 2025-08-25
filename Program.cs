using CampusLoveDarwinDaniel.Shared.Context;
using CampusLoveDarwinDaniel.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión de appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MySqlDB");

// Registrar DbContext
builder.Services.AddDbContext<CampusLoveDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registrar el verificador de conexión
builder.Services.AddTransient<DbConnectionChecker>();

var app = builder.Build();

// Probar conexión a MySQL al iniciar
using (var scope = app.Services.CreateScope())
{
    var dbChecker = scope.ServiceProvider.GetRequiredService<DbConnectionChecker>();
    dbChecker.CheckConnection();
}

app.MapGet("/", () => "✅ CampusLove corriendo con MySQL local!");

app.Run();
