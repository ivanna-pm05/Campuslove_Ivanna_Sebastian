using System;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Interfaces;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.UI
{
    public class MenuPerfiles
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IInteraccionService _interaccionService;
        private readonly int _usuarioId; // Usuario activo (logueado)

        public MenuPerfiles(
            IUsuarioService usuarioService,
            IInteraccionService interaccionService,
            int usuarioId)
        {
            _usuarioService = usuarioService;
            _interaccionService = interaccionService;
            _usuarioId = usuarioId;
        }

        public async Task RenderMenu()
        {
            bool salir = false;

            // Obtengo todos los usuarios menos el actual
            var todos = await _usuarioService.ConsultarUsuarioAsync();
            var perfiles = todos.Where(u => u.Id != _usuarioId).ToList();

            if (!perfiles.Any())
            {
                Console.WriteLine("⚠ No hay perfiles para mostrar.");
                Console.WriteLine("Presione una tecla para regresar...");
                Console.ReadKey();
                return;
            }

            int indice = 0;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("+==================================+");
                Console.WriteLine("|          MENÚ DE PERFILES         |");
                Console.WriteLine("+==================================+");

                var perfil = perfiles[indice];
                Console.WriteLine($"ID: {perfil.Id}");
                Console.WriteLine($"Nombre: {perfil.Nombre}");
                Console.WriteLine($"Edad: {perfil.Edad}");
                Console.WriteLine($"Género: {perfil.Genero}");
                Console.WriteLine($"Carrera: {perfil.Carrera}");
                Console.WriteLine($"Intereses: {perfil.Intereses}");
                Console.WriteLine($"Frases: {perfil.Frases}");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("| 1. Dar Like                      |");
                Console.WriteLine("| 2. Dar Dislike                   |");
                Console.WriteLine("| 3. Siguiente perfil              |");
                Console.WriteLine("| 4. Volver al menú principal      |");
                Console.WriteLine("+==================================+");
                Console.Write("Seleccione una opción: ");

                int opcion = LeerEntero("-> ");

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("👍 Has dado LIKE a este perfil.");
                        await _interaccionService.RegistrarLikeAsync(_usuarioId, perfil.Id);
                        break;

                    case 2:
                        Console.WriteLine("👎 Has dado DISLIKE a este perfil.");
                        await _interaccionService.RegistrarDislikeAsync(_usuarioId, perfil.Id);
                        break;

                    case 3:
                        indice = (indice + 1) % perfiles.Count;
                        break;

                    case 4:
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("⚠ Opción inválida, intente nuevamente.");
                        break;
                }

                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        private int LeerEntero(string mensaje)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje + " ");
                if (int.TryParse(Console.ReadLine(), out valor))
                    return valor;
                Console.WriteLine("⚠️ Ingrese un número válido.");
            }
        }
    }
} 
