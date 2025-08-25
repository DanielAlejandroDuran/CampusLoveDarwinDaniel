using Microsoft.EntityFrameworkCore;
using CampusLoveDarwinDaniel.Shared.Context;

namespace CampusLoveDarwinDaniel.Shared.Helpers
{
    public class DbConnectionChecker
    {
        private readonly CampusLoveDbContext _context;

        public DbConnectionChecker(CampusLoveDbContext context)
        {
            _context = context;
        }

        public void CheckConnection()
        {
            try
            {
                if (_context.Database.CanConnect())
                {
                    Console.WriteLine("✅ Conectado a la base de datos MySQL correctamente.");
                }
                else
                {
                    Console.WriteLine("❌ No se pudo conectar a la base de datos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error al verificar la conexión: {ex.Message}");
            }
        }
    }
}
