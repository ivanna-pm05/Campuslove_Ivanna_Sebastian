using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;
using System;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.UI
{
    public class MenuUsuarios
    {
        private readonly AppDbContext _context;
        private readonly UsuarioRepository _usuarioRepo;
        private readonly UsuarioService _service;

        public MenuUsuarios(AppDbContext context)
        {
            _context = context;
            _usuarioRepo = new UsuarioRepository(_context);
            _service = new UsuarioService(_usuarioRepo);
        }
        public async Task RenderMenu()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("+===============+");
                Console.WriteLine("|        MENÚ DE USUARIOS       |");
                Console.WriteLine("+=============+");
                Console.WriteLine("| 1. Registrar Usuario          |");
                Console.WriteLine("| 2. Editar Usuario             |");
                Console.WriteLine("| 3. Eliminar Usuario           |");
                Console.WriteLine("| 4. Buscar Usuario             |");
                Console.WriteLine("| 5. Regresar al menú principal |");
                Console.WriteLine("+=====+");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("==> Registrar Usuario");
                        await _service.RegistrarUsuarioAsync();
                        break;

                    case "2":
                        Console.WriteLine("==> Editar Usuario");
                        await _service.EditarUsuarioAsync();
                        break;

                    case "3":
                        Console.WriteLine("==> Eliminar Usuario");
                        await _service.EliminarUsuarioAsync();
                        break;

                    case "4":
                        Console.WriteLine("==> Buscar Usuario");
                        await _service.BuscarUsuarioAsync();
                        break;

                    case "5":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("⚠ Opción inválida, presione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                }
            }
        }
    }
}