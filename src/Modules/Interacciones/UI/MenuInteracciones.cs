using System;
using System.Threading.Tasks;
using System.Linq;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.UI
{
    public class MenuInteracciones
    {
        private readonly AppDbContext _context;
        private readonly InteraccionService _interaccionService;
        private readonly UsuarioService _usuarioService;

        public MenuInteracciones(AppDbContext context)
        {
            _context = context;

            var interaccionRepo = new InteraccionRepository(_context);
            _interaccionService = new InteraccionService(interaccionRepo);

            var usuarioRepo = new UsuarioRepository(_context);
            _usuarioService = new UsuarioService(usuarioRepo);
        }

        public async Task RenderMenu(int idUsuarioLogueado)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("+=================================+");
                Console.WriteLine("|     MENÚ DE INTERACCIONES       |");
                Console.WriteLine("+=================================+");
                Console.WriteLine("| 1. Dar Like 👍                  |");
                Console.WriteLine("| 2. Dar Dislike 👎               |");
                Console.WriteLine("| 3. Ver mis interacciones 📋     |");
                Console.WriteLine("| 4. Regresar al menú principal ⬅ |");
                Console.WriteLine("+=================================+");
                Console.ResetColor();

                int opcion = LeerEntero("-> ");

                switch (opcion)
                {
                    case 1: // Dar Like
                        await MostrarUsuariosDisponiblesYRegistrarAsync(idUsuarioLogueado, true);
                        break;
                    case 2: // Dar Dislike
                        await MostrarUsuariosDisponiblesYRegistrarAsync(idUsuarioLogueado, false);
                        break;
                    case 3: // Ver interacciones
                        await MostrarInteraccionesAsync(idUsuarioLogueado);
                        break;
                    case 4:
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("⚠️ Opción inválida, presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task MostrarUsuariosDisponiblesYRegistrarAsync(int idUsuarioLogueado, bool esLike)
        {
            Console.Clear();
            Console.WriteLine(esLike ? "+=====================+" : "+========================+");
            Console.WriteLine(esLike ? "|     Dar LIKE 👍     |" : "|     Dar DISLIKE 👎 |");
            Console.WriteLine(esLike ? "+=====================+" : "+========================+");

            // Traer todos los usuarios excepto el logueado
            var usuarios = (await _usuarioService.ConsultarUsuarioAsync())
                .Where(u => u != null && u.Id != idUsuarioLogueado)
                .ToList();

            if (!usuarios.Any())
            {
                Console.WriteLine("⚠ No hay usuarios disponibles para interactuar.");
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n📋 Usuarios disponibles:");
            Console.WriteLine("+-----+----------------------+");
            Console.WriteLine("| ID  | Nombre               |");
            Console.WriteLine("+-----+----------------------+");

            foreach (var u in usuarios)
            {
                Console.WriteLine($"| {u.Id,-3} | {u.Nombre,-20} |");
            }
            Console.WriteLine("+-----+----------------------+");

            int idDestino = LeerEntero("ID del usuario destino: ");
            var usuarioDestino = await _usuarioService.ObtenerUsuarioAsync(idDestino);

            if (usuarioDestino != null)
            {
                if (esLike)
                {
                    await _interaccionService.RegistrarLikeAsync(idUsuarioLogueado, idDestino);
                    Console.WriteLine($"✅ Has dado LIKE a {usuarioDestino.Nombre}");
                }
                else
                {
                    await _interaccionService.RegistrarDislikeAsync(idUsuarioLogueado, idDestino);
                    Console.WriteLine($"❌ Has dado DISLIKE a {usuarioDestino.Nombre}");
                }
            }
            else
            {
                Console.WriteLine("❌ Usuario no encontrado.");
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        private async Task MostrarInteraccionesAsync(int idUsuarioLogueado)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("+=============================+");
            Console.WriteLine("|   Tus Interacciones 📋      |");
            Console.WriteLine("+=============================+");
            Console.ResetColor();

            var todasInteracciones = await _interaccionService.ObtenerInteraccionesDeUsuarioAsync(idUsuarioLogueado);

            // Sección: Interacciones INICIADAS por el usuario
            var iniciadas = todasInteracciones
                .Where(i => i != null && i.IdUsuarioOrigen == idUsuarioLogueado)
                .ToList();

            Console.WriteLine("🔹 Interacciones que has hecho:");
            if (!iniciadas.Any())
            {
                Console.WriteLine("  ⚠ No has interactuado con ningún usuario aún.");
            }
            else
            {
                foreach (var i in iniciadas)
                {
                    var usuarioDestino = await _usuarioService.ObtenerUsuarioAsync(i.IdUsuarioDestino);
                    string nombreDestino = usuarioDestino?.Nombre ?? i.IdUsuarioDestino.ToString();
                    string tipo = i.TipoInteraccion == Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE
                        ? "👍 LIKE"
                        : "👎 DISLIKE";
                    Console.WriteLine($"  ➡️ {tipo} | Hacia: {nombreDestino} | Fecha: {i.FechaInteraccion}");
                }
            }

            Console.WriteLine("\n🔹 Interacciones que recibiste:");
            // Sección: Interacciones RECIBIDAS por el usuario
            var recibidas = todasInteracciones
                .Where(i => i != null && i.IdUsuarioDestino == idUsuarioLogueado)
                .ToList();

            if (!recibidas.Any())
            {
                Console.WriteLine("  ⚠ Nadie ha interactuado contigo aún.");
            }
            else
            {
                foreach (var i in recibidas)
                {
                    var usuarioOrigen = await _usuarioService.ObtenerUsuarioAsync(i.IdUsuarioOrigen);
                    string nombreOrigen = usuarioOrigen?.Nombre ?? i.IdUsuarioOrigen.ToString();
                    string tipo = i.TipoInteraccion == Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE
                        ? "👍 LIKE"
                        : "👎 DISLIKE";
                    Console.WriteLine($"  ➡️ {tipo} | De: {nombreOrigen} | Fecha: {i.FechaInteraccion}");
                }
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
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
