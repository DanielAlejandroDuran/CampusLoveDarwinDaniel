using CampusLoveDarwinDaniel.Shared.Context;
using System;

namespace CampusLoveDarwinDaniel.Shared.Helpers
{
    /// <summary>
    /// Clase dedicada a inicializar y verificar la conexión con la base de datos.
    /// Esto mantiene el archivo Program.cs más limpio.
    /// </summary>
    public class DatabaseInitializer
    {
        private readonly CampusLoveDbContext _context;

        public DatabaseInitializer(CampusLoveDbContext context)
        {
            _context = context;
        }

        public void CheckConnection()
        {
            Console.WriteLine("Verificando conexión con la base de datos...");
            try
            {
                if (_context.Database.CanConnect())
                {
                    Console.WriteLine("✅ Conexión a la base de datos MySQL exitosa.");
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
            Console.WriteLine(new string('-', 50));
        }
    }
}