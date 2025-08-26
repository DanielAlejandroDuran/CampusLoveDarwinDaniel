using System;
using System.Threading.Tasks;
using CampusLove.Application.Services;
using CampusLove.Domain.Ports;
using System.Linq;

namespace CampusLove.Adapters.ConsoleApp
{
    public class ConsoleApp
    {
        private readonly MatchService _service;
        private readonly IUsuarioRepository _usuarios;
        private int? _currentUserId = null;

        public ConsoleApp(MatchService service, IUsuarioRepository usuarios)
        {
            _service = service;
            _usuarios = usuarios;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Campus Love ===");
                if (_currentUserId == null)
                {
                    Console.WriteLine("1) Iniciar sesión");
                    Console.WriteLine("2) Registrarse");
                    Console.WriteLine("0) Salir");
                    Console.Write("Opción: ");
                    var opt = Console.ReadLine();
                    switch (opt)
                    {
                        case "1": await LoginFlow(); break;
                        case "2": await RegisterFlow(); break;
                        case "0": return;
                        default: Console.WriteLine("Opción inválida."); break;
                    }
                }
                else
                {
                    Console.WriteLine($"Conectado como usuario {_currentUserId}");
                    Console.WriteLine("1) Ver perfiles y dar Like/Dislike");
                    Console.WriteLine("2) Ver mis coincidencias");
                    Console.WriteLine("3) Estadísticas (Top likes)");
                    Console.WriteLine("4) Cerrar sesión");
                    Console.WriteLine("0) Salir");
                    Console.Write("Opción: ");
                    var opt = Console.ReadLine();
                    switch (opt)
                    {
                        case "1": await BrowseFlow(); break;
                        case "2": await MatchesFlow(); break;
                        case "3": await StatsFlow(); break;
                        case "4": _currentUserId = null; break;
                        case "0": return;
                        default: Console.WriteLine("Opción inválida."); break;
                    }
                }
                Console.WriteLine("Presiona ENTER para continuar...");
                Console.ReadLine();
            }
        }

        private async Task LoginFlow()
        {
            Console.Write("UsuarioId: ");
            int.TryParse(Console.ReadLine(), out int id);
            Console.Write("Contraseña (dejar vacío si no tienes): ");
            var pass = ReadPassword();

            var res = await _service.AuthenticateAsync(id, pass);
            Console.WriteLine(res.Message);
            if (res.Success) _currentUserId = res.Usuario?.UsuarioId;
        }

        private async Task RegisterFlow()
        {
            Console.Write("Nombre: ");
            var nombre = Console.ReadLine() ?? "";
            Console.Write("Edad: ");
            int.TryParse(Console.ReadLine(), out int edad);
            Console.Write("Género: ");
            var genero = Console.ReadLine() ?? "";
            Console.Write("Carrera: ");
            var carrera = Console.ReadLine() ?? "";
            Console.Write("Frase perfil: ");
            var frase = Console.ReadLine();

            Console.WriteLine("Intereses (separados por coma): ");
            var interesesCsv = Console.ReadLine() ?? "";
            var intereses = interesesCsv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            Console.Write("Contraseña (opcional): ");
            var pass = ReadPassword();

            var u = await _service.RegisterUserAsync(nombre, edad, genero, carrera, frase, intereses, pass);
            Console.WriteLine($"Usuario registrado con ID {u.UsuarioId}");
        }

        private async Task BrowseFlow()
        {
            if (_currentUserId == null) { Console.WriteLine("Debes iniciar sesión primero."); return; }
            int id = _currentUserId.Value;
            var perfiles = await _usuarios.GetProfilesForAsync(id);
            if (!perfiles.Any()) { Console.WriteLine("No hay perfiles disponibles."); return; }

            foreach (var p in perfiles)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Id: {p.UsuarioId} | {p.Nombre} | {p.Edad} años | {p.Carrera}");
                Console.WriteLine($"Intereses: {string.Join(", ", p.Intereses.Select(i => i.Interes))}");
                Console.WriteLine(p.FrasePerfil);
                Console.WriteLine("1) Like  2) Dislike  0) Siguiente");
                var opt = Console.ReadLine();
                if (opt == "1")
                {
                    var res = await _service.LikeAsync(id, p.UsuarioId);
                    Console.WriteLine(res.Message);
                }
                else if (opt == "2")
                {
                    var res = await _service.DislikeAsync(id, p.UsuarioId);
                    Console.WriteLine(res.Message);
                }
                else
                {
                    // siguiente
                }
            }
            Console.WriteLine("No hay más perfiles.");
        }

        private async Task MatchesFlow()
        {
            if (_currentUserId == null) { Console.WriteLine("Debes iniciar sesión primero."); return; }
            var matches = await _service.GetMatchesForAsync(_currentUserId.Value);
            if (matches == null || !matches.Any()) Console.WriteLine("No tienes coincidencias todavía.");
            else
            {
                Console.WriteLine("== Tus matches ==");
                foreach (var m in matches)
                    Console.WriteLine($"Match -> {m.UsuarioId} {m.Nombre} | {m.Carrera}");
            }
        }

        private async Task StatsFlow()
        {
            var top = await _service.GetTopLikedAsync(5);
            Console.WriteLine("Top usuarios por likes recibidos:");
            foreach (var t in top)
                Console.WriteLine($"Usuario {t.UsuarioId} - Likes: {t.LikesReceived}");
        }

        private static string? ReadPassword()
        {
            var pass = "";
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return pass;
        }
    }
}
