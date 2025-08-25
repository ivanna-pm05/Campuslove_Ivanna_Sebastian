using System;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.UI
{
    public class MenuPerfiles
    {
        private readonly AppDbContext _context;
        private readonly UsuarioRepository _usuarioRepo;
        private readonly UsuarioService _usuarioService;
        private readonly InteraccionRepository _interaccionRepo;
        private readonly InteraccionService _interaccionService;
        private readonly int _usuarioId; // Usuario activo (logueado)

        public MenuPerfiles(AppDbContext context, int usuarioId)
        {
            _context = context;
            _usuarioRepo = new UsuarioRepository(_context);
            _usuarioService = new UsuarioService(_usuarioRepo);
            _interaccionRepo = new InteraccionRepository();
            _interaccionService = new InteraccionService(_interaccionRepo);
            _usuarioId = usuarioId;
        }
        public async Task RenderMenu()
        {
            bool salir = false;

            // Obtengo todos los usuarios excepto el actual
            var perfiles = _usuarioRepo.GetAll().Where(u => u.Id != _usuarioId).ToList();
            int indice = 0;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("+==================================+");
                Console.WriteLine("|      SUBMENÚ: VER PERFILES       |");
                Console.WriteLine("+==================================+");

                if (!perfiles.Any())
                {
                    Console.WriteLine("⚠ No hay más perfiles disponibles.");
                    Console.WriteLine("Presione cualquier tecla para regresar...");
                    Console.ReadKey();
                    return;
                }

                // Mostrar perfil actual
                var perfil = perfiles[indice];
                Console.WriteLine($"ID: {perfil.Id}");
                Console.WriteLine($"Nombre: {perfil.Nombre}");
                Console.WriteLine($"Edad: {perfil.Edad}");
                Console.WriteLine($"Género: {perfil.Genero}");
                Console.WriteLine($"Carrera: {perfil.Carrera}");
                Console.WriteLine($"Intereses: {perfil.Intereses}");
                Console.WriteLine($"Frase: {perfil.FrasePerfil}");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("| 1. Dar Like                      |");
                Console.WriteLine("| 2. Dar Dislike                   |");
                Console.WriteLine("| 3. Siguiente perfil              |");
                Console.WriteLine("| 4. Volver al menú principal      |");
                Console.WriteLine("+==================================+");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("👍 Has dado LIKE a este perfil.");
                        await _interaccionService.RegistrarLikeAsync(_usuarioId, perfil.Id);
                        break;

                    case "2":
                        Console.WriteLine("👎 Has dado DISLIKE a este perfil.");
                        await _interaccionService.RegistrarDislikeAsync(_usuarioId, perfil.Id);
                        break;

                    case "3":
                        indice = (indice + 1) % perfiles.Count; // Mover al siguiente
                        break;

                    case "4":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("⚠ Opción inválida.");
                        break;
                }

                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}