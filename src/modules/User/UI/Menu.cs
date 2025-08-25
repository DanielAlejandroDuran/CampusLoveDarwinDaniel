using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusLoveDarwinDaniel.Modules.Matches.Application.Services;
using CampusLoveDarwinDaniel.Modules.Interactions.Application.Services;
using CampusLoveDarwinDaniel.Modules.Interactions.Domain.Entities;
using CampusLoveDarwinDaniel.Modules.Users.Application.Services;
namespace CampusLoveDarwinDaniel.src.modules.User.UI;

public class Menu
{
    private readonly UserService _userService;
    private readonly InteractionService _interactionService;
    private readonly MatchService _matchService;

    public Menu(UserService userService, InteractionService interactionService, MatchService matchService)
    {
        _userService = userService;
        _interactionService = interactionService;
        _matchService = matchService;
    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("\n===== Campus Love =====");
            Console.WriteLine("1. Registrarse");
            Console.WriteLine("2. Ver perfiles y dar Like/Dislike");
            Console.WriteLine("3. Ver mis coincidencias");
            Console.WriteLine("4. Ver estadísticas");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    RegisterUser();
                    break;
                case "2":
                    BrowseProfiles();
                    break;
                case "3":
                    ShowMatches();
                    break;
                case "4":
                    ShowStatistics();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }

    private void RegisterUser()
    {
        // Aquí se llama al Factory para crear un usuario
    }

    private void BrowseProfiles()
    {
        // Aquí muestras usuarios y permites Like/Dislike
    }

    private void ShowMatches()
    {
        // Mostrar coincidencias
    }

    private void ShowStatistics()
    {
        // Llamar a consultas LINQ en EF Core
    }
}
