using System;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.UI
{
    public class MenuMatches
    {
        private readonly AppDbContext _context;
        private readonly InteraccionService _interaccionService;
        private readonly UsuarioService _usuarioService;

        public MenuMatches(AppDbContext context)
        {
            _context = context;
            _interaccionService = new InteraccionService(new InteraccionRepository(_context));
            _usuarioService = new UsuarioService(new UsuarioRepository(_context));
        }

        public async Task RenderMenu(int idUsuarioLogueado)
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("+=================================+");
                Console.WriteLine("|       💌 MIS COINCIDENCIAS 💌      |");
                Console.WriteLine("+=================================+");
                Console.ResetColor();

                Console.WriteLine("| 1. 💖 Ver Matches Mutuos          |");
                Console.WriteLine("| 2. 💌 Ver Likes Recibidos         |");
                Console.WriteLine("| 3. 🔙 Regresar al menú principal  |");
                Console.WriteLine("+=================================+");

                int opcion = LeerEntero("-> ");

                switch (opcion)
                {
                    case 1:
                        await MostrarMatchesMutuosAsync(idUsuarioLogueado);
                        break;

                    case 2:
                        await MostrarLikesRecibidosAsync(idUsuarioLogueado);
                        break;

                    case 3:
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("⚠️ Opción inválida, presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task MostrarMatchesMutuosAsync(int idUsuarioLogueado)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("+===============================+");
            Console.WriteLine("|       💖 MATCHES MUTUOS 💖     |");
            Console.WriteLine("+===============================+");
            Console.ResetColor();

            var interacciones = await _interaccionService.ObtenerInteraccionesDeUsuarioAsync(idUsuarioLogueado);

            var matches = interacciones
                .Where(i => i.TipoInteraccion == Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE)
                .Where(i => interacciones.Any(j =>
                    j.IdUsuarioOrigen == i.IdUsuarioDestino &&
                    j.IdUsuarioDestino == i.IdUsuarioOrigen &&
                    j.TipoInteraccion == Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE))
                .Where(i => i.IdUsuarioOrigen == idUsuarioLogueado)
                .ToList();

            if (!matches.Any())
            {
                Console.WriteLine("⚠️ No tienes matches aún.");
            }
            else
            {
                Console.WriteLine("🎉 Matches encontrados:");
                foreach (var m in matches)
                {
                    var usuarioDestino = await _usuarioService.ObtenerUsuarioAsync(m.IdUsuarioDestino);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"➡️ {usuarioDestino?.Nombre} | Fecha: {m.FechaInteraccion}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        private async Task MostrarLikesRecibidosAsync(int idUsuarioLogueado)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("+===============================+");
            Console.WriteLine("|       💌 LIKES RECIBIDOS 💌     |");
            Console.WriteLine("+===============================+");
            Console.ResetColor();

            var interacciones = await _interaccionService.ObtenerInteraccionesDeUsuarioAsync(idUsuarioLogueado);

            var likesRecibidos = interacciones
                .Where(i => i.IdUsuarioDestino == idUsuarioLogueado && i.TipoInteraccion == Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE)
                .Where(i => !interacciones.Any(j =>
                    j.IdUsuarioDestino == i.IdUsuarioOrigen &&
                    j.IdUsuarioOrigen == i.IdUsuarioDestino &&
                    j.TipoInteraccion == Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE))
                .ToList();

            if (!likesRecibidos.Any())
            {
                Console.WriteLine("⚠️ No has recibido likes aún.");
            }
            else
            {
                Console.WriteLine("💌 Likes recibidos:");
                foreach (var like in likesRecibidos)
                {
                    var usuarioOrigen = await _usuarioService.ObtenerUsuarioAsync(like.IdUsuarioOrigen);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"➡️ {usuarioOrigen?.Nombre} | Fecha: {like.FechaInteraccion}");
                    Console.ResetColor();
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
