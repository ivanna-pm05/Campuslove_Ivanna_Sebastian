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
    public class MenuEstadisticas
    {
        private readonly AppDbContext _context;
        private readonly InteraccionService _interaccionService;
        private readonly UsuarioService _usuarioService;

        public MenuEstadisticas(AppDbContext context)
        {
            _context = context;

            var interaccionRepo = new InteraccionRepository(_context);
            _interaccionService = new InteraccionService(interaccionRepo);

            var usuarioRepo = new UsuarioRepository(_context);
            _usuarioService = new UsuarioService(usuarioRepo);
        }

        public async Task RenderMenu(int idUsuarioLogueado)
        {
            if (idUsuarioLogueado <= 0)
            {
                Console.WriteLine("⚠ Debes iniciar sesión primero para ver las estadísticas.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            var todosUsuarios = await _usuarioService.ConsultarUsuarioAsync();
            var todasInteracciones = new System.Collections.Generic.List<Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.Interaccion>();

            foreach (var u in todosUsuarios)
            {
                if (u != null)
                {
                    var interaccionesUsuario = await _interaccionService.ObtenerInteraccionesDeUsuarioAsync(u.Id);
                    todasInteracciones.AddRange(interaccionesUsuario);
                }
            }

            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║          📊 ESTADÍSTICAS DEL SISTEMA      ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝\n");

            // Usuario con más likes recibidos
            var usuarioMasLikes = todosUsuarios
                .Select(u => new
                {
                    Usuario = u,
                    LikesRecibidos = todasInteracciones.Count(i =>
                        i.IdUsuarioDestino == u.Id &&
                        i.TipoInteraccion == Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE)
                })
                .OrderByDescending(x => x.LikesRecibidos)
                .FirstOrDefault();

            // Usuario con más matches mutuos
            var matchesMutuos = todosUsuarios
                .Select(u => new
                {
                    Usuario = u,
                    Count = todasInteracciones.Count(i =>
                        i.IdUsuarioOrigen == u.Id &&
                        i.TipoInteraccion == Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE &&
                        todasInteracciones.Any(j =>
                            j.IdUsuarioOrigen == i.IdUsuarioDestino &&
                            j.IdUsuarioDestino == i.IdUsuarioOrigen &&
                            j.TipoInteraccion == Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE))
                })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            Console.WriteLine("┌─────────────────────────────┐");
            Console.WriteLine("│ 🎯 Usuario con más likes    │");
            Console.WriteLine("├───────────────┬─────────────┤");
            if (usuarioMasLikes != null)
                Console.WriteLine($"│ {usuarioMasLikes.Usuario.Nombre,-13} │ {usuarioMasLikes.LikesRecibidos,11} │");
            else
                Console.WriteLine("│ No hay datos                 │");
            Console.WriteLine("└───────────────┴─────────────┘\n");

            Console.WriteLine("┌─────────────────────────────┐");
            Console.WriteLine("│ 💞 Usuario con más matches  │");
            Console.WriteLine("├───────────────┬─────────────┤");
            if (matchesMutuos != null)
                Console.WriteLine($"│ {matchesMutuos.Usuario.Nombre,-13} │ {matchesMutuos.Count,11} │");
            else
                Console.WriteLine("│ No hay datos                 │");
            Console.WriteLine("└───────────────┴─────────────┘");

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
