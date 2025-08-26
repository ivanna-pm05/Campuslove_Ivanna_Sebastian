using System;
using System.Threading.Tasks;
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
        readonly InteraccionRepository _interaccionRepo = null!;
        readonly InteraccionService interaccionService = null!;
        readonly UsuarioRepository _usuarioRepo = null!;
        readonly UsuarioService usuarioService = null!;

        public MenuInteracciones(AppDbContext context)
        {
            _context = context;
            _interaccionRepo = new InteraccionRepository(_context);
            interaccionService = new InteraccionService(_interaccionRepo);
            _usuarioRepo = new UsuarioRepository(_context);
            usuarioService = new UsuarioService(_usuarioRepo);
        }

        public async Task RenderMenu(int idUsuarioLogueado)
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("+=================================+");
                Console.WriteLine("|     MENÚ DE INTERACCIONES       |");
                Console.WriteLine("+=================================+");
                Console.WriteLine("| 1. Dar Like 👍                   |");
                Console.WriteLine("| 2. Dar Dislike 👎                |");
                Console.WriteLine("| 3. Ver mis interacciones 📋      |");
                Console.WriteLine("| 4. Regresar al menú principal ⬅ |");
                Console.WriteLine("+=================================+");
                Console.WriteLine("\n📋 Usuarios disponibles:");
                Console.WriteLine("+-----+----------------------+");
                Console.WriteLine("| ID  | Nombre               |");
                Console.WriteLine("+-----+----------------------+");

                var todosUsuario = await usuarioService.ConsultarUsuarioAsync();
                foreach (var usuario in todosUsuario)
                {
                    if (usuario != null && usuario.Id != idUsuarioLogueado) // No mostrar al usuario logueado ni nulls
                    {
                        Console.WriteLine($"| {usuario.Id,-3} | {usuario.Nombre,-20} |");
                    }
                }
                Console.WriteLine("+-----+----------------------+");
                Console.Write("Seleccione una opción: ");

                int opcion = LeerEntero("-> ");

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("+=====================+");
                        Console.WriteLine("|     Dar LIKE 👍     |");
                        Console.WriteLine("+=====================+");
                        Console.WriteLine("\n📋 Usuarios disponibles:");
                        Console.WriteLine("+-----+----------------------+");
                        Console.WriteLine("| ID  | Nombre               |");
                        Console.WriteLine("+-----+----------------------+");

                        var todosUsuarios = await usuarioService.ConsultarUsuarioAsync();
                        foreach (var usuario in todosUsuarios)
                        {
                            if (usuario != null && usuario.Id != idUsuarioLogueado) // No mostrar al usuario logueado ni nulls
                            {
                                Console.WriteLine($"| {usuario.Id,-3} | {usuario.Nombre,-20} |");
                            }
                        }
                        Console.WriteLine("+-----+----------------------+");
                        int idLike = LeerEntero("ID del usuario destino: ");
                        Usuario? usuarioLike = await usuarioService.ObtenerUsuarioAsync(idLike);
                        if (usuarioLike != null)
                        {
                            await interaccionService.RegistrarLikeAsync(idUsuarioLogueado, idLike);
                            Console.WriteLine($"✅ Has dado LIKE a {usuarioLike.Nombre}");
                        }
                        else
                        {
                            Console.WriteLine("❌ Usuario no encontrado.");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("+========================+");
                        Console.WriteLine("|     Dar DISLIKE 👎     |");
                        Console.WriteLine("+========================+");
                        Console.WriteLine("\n📋 Usuarios disponibles:");
                        Console.WriteLine("+-----+----------------------+");
                        Console.WriteLine("| ID  | Nombre               |");
                        Console.WriteLine("+-----+----------------------+");

                        var todosUsuariosDislike = await usuarioService.ConsultarUsuarioAsync();
                        foreach (var usuario in todosUsuariosDislike)
                        {
                            if (usuario != null && usuario.Id != idUsuarioLogueado) // No mostrar al usuario logueado ni nulls
                            {
                                Console.WriteLine($"| {usuario.Id,-3} | {usuario.Nombre,-20} |");
                            }
                        }
                        Console.WriteLine("+-----+----------------------+");
                        int idDislike = LeerEntero("ID del usuario destino: ");
                        Usuario? usuarioDislike = await usuarioService.ObtenerUsuarioAsync(idDislike);
                        if (usuarioDislike != null)
                        {
                            await interaccionService.RegistrarDislikeAsync(idUsuarioLogueado, idDislike);
                            Console.WriteLine($"❌ Has dado DISLIKE a {usuarioDislike.Nombre}");
                        }
                        else
                        {
                            Console.WriteLine("❌ Usuario no encontrado.");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("+=============================+");
                        Console.WriteLine("|   Tus Interacciones 📋      |");
                        Console.WriteLine("+=============================+");
                        var interacciones = await interaccionService.ObtenerInteraccionesDeUsuarioAsync(idUsuarioLogueado);
                        foreach (var i in interacciones)
                        {
                            string tipo = i?.TipoInteraccion == 
                                Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities.TipoInteraccion.LIKE 
                                ? "👍 LIKE" : "👎 DISLIKE";
                            Console.WriteLine($"➡️ {tipo} | De: {i?.IdUsuarioOrigen}  → Hacia: {i?.IdUsuarioDestino} | Fecha: {i?.FechaInteraccion}");
                        }
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
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
