using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.UI
{
    public class MenuUsuarios
    {
        private readonly AppDbContext _context;
        private readonly UsuarioRepository _usuariorepo;
        private readonly UsuarioService _service;
        private Usuario? _usuarioLogueado = null;

        public MenuUsuarios(AppDbContext context)
        {
            _context = context;
            _usuariorepo = new UsuarioRepository(_context);
            _service = new UsuarioService(_usuariorepo);
        }

        public async Task RenderMenu()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("+===============================+");
                Console.WriteLine("|        MENÚ DE USUARIOS       |");
                Console.WriteLine("+===============================+");
                Console.ResetColor();
                Console.WriteLine("| 1. Registrar Usuario          |");
                Console.WriteLine("| 2. Iniciar Sesión             |");
                Console.WriteLine("| 3. Ingresar/Completar Perfil  |");
                Console.WriteLine("| 4. Eliminar Usuario           |");
                Console.WriteLine("| 5. Buscar Usuario             |");
                Console.WriteLine("| 6. Editar Perfil              |");
                Console.WriteLine("| 7. Regresar al menú principal |");
                Console.WriteLine("+===============================+");
                Console.Write("Seleccione una opción: ");

                int opcion = LeerEntero("-> ");

                switch (opcion)
                {
                    case 1:
                        await RegistrarUsuarioAsync();
                        break;

                    case 2:
                        await IniciarSesionAsync();
                        break;

                    case 3:
                        if (!VerificarLogin()) break;
                        await CompletarPerfilAsync();
                        break;

                    case 4:
                        if (!VerificarLogin()) break;
                        await EliminarUsuarioAsync();
                        break;

                    case 5:
                        await BuscarUsuarioAsync();
                        break;

                    case 6:
                        if (!VerificarLogin()) break;
                        await EditarPerfilAsync();
                        break;

                    case 7:
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("⚠ Opción inválida, presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private bool VerificarLogin()
        {
            if (_usuarioLogueado == null)
            {
                Console.WriteLine("⚠ Debes iniciar sesión primero.");
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        private async Task RegistrarUsuarioAsync()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("+=====================+");
            Console.WriteLine("|  Registrar Usuario  |");
            Console.WriteLine("+=====================+");
            Console.ResetColor();

            Console.Write("Nombre Usuario: ");
            string? nombre = Console.ReadLine();
            Console.Write("Contraseña: ");
            string? clave = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(clave))
            {
                Console.WriteLine("❌ Nombre y contraseña son obligatorios.");
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            try
            {
                await _service.RegistrarUsuarioAsync(nombre, clave);
                Console.WriteLine("✅ Usuario registrado. Ahora inicia sesión para completar tu perfil.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        private async Task IniciarSesionAsync()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║             Iniciar Sesión            ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("Nombre de usuario: ");
            string nombre = Console.ReadLine()!;
            Console.Write("Contraseña: ");
            string clave = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(clave))
            {
                Console.WriteLine("❌ Nombre y contraseña son obligatorios.");
                Console.ReadKey();
                return;
            }

            try
            {
                var usuario = await _service.LoginAsync(nombre, clave);
                if (usuario != null)
                {
                    _usuarioLogueado = usuario;
                    Console.WriteLine($"✅ Bienvenido, {usuario.Nombre}.");

                    if (!usuario.PerfilCompleto)
                        Console.WriteLine("⚠ Tu perfil está incompleto. Ve a 'Ingresar/Completar Perfil'.");
                }
                else
                {
                    Console.WriteLine("❌ Credenciales incorrectas.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.ReadKey();
        }

        private async Task CompletarPerfilAsync()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+=======================+");
            Console.WriteLine("|      Datos Perfil     |");
            Console.WriteLine("+=======================+");
            Console.ResetColor();

            int edad = LeerEntero("Edad: ");
            Console.Write("Género (F/M/O): ");
            string? genero = Console.ReadLine();
            Console.Write("Carrera: ");
            string? carrera = Console.ReadLine();
            Console.Write("Intereses: ");
            string? intereses = Console.ReadLine();
            Console.Write("Frases: ");
            string? frases = Console.ReadLine();

            try
            {
                await _service.CompletarPerfilAsync(_usuarioLogueado!.Id, edad, genero!, carrera!, intereses!, frases!);
                Console.WriteLine("✅ Perfil completado exitosamente.");
                _usuarioLogueado = await _service.ObtenerUsuarioAsync(_usuarioLogueado.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        private async Task EliminarUsuarioAsync()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("+=====================+");
            Console.WriteLine("|  Eliminar Usuario   |");
            Console.WriteLine("+=====================+");
            Console.ResetColor();

            await MostrarListaUsuariosAsync();

            int idDel = LeerEntero("ID del Usuario a eliminar: ");
            await _service.EliminarUsuario(idDel);
            Console.WriteLine("🗑️ Usuario Eliminado.");
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        private async Task BuscarUsuarioAsync()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════╗");
            Console.WriteLine("║      🔍 Buscar Usuario     ║");
            Console.WriteLine("╚══════════════════════╝");
            Console.ResetColor();

            await MostrarListaUsuariosAsync();

            int id = LeerEntero("ID del Usuario a buscar: ");
            Usuario? usuarioEncontrado = await _service.ObtenerUsuarioAsync(id);
            if (usuarioEncontrado != null)
                Console.WriteLine($"👤 {usuarioEncontrado.Nombre} - {usuarioEncontrado.Edad} - {usuarioEncontrado.Genero} - {usuarioEncontrado.Carrera} - {usuarioEncontrado.Intereses} - {usuarioEncontrado.Frases}");
            else
                Console.WriteLine("❌ Usuario no encontrado.");

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        private async Task EditarPerfilAsync()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("+==================+");
            Console.WriteLine("|   Editar Perfil  |");
            Console.WriteLine("+==================+");
            Console.ResetColor();

            int nuevaEdad = LeerEntero("Nueva edad: ");
            Console.Write("Nuevo Género (F/M/O): ");
            string? nuevoGenero = Console.ReadLine();
            Console.Write("Nueva Carrera: ");
            string? nuevaCarrera = Console.ReadLine();
            Console.Write("Nuevos Intereses: ");
            string? nuevosIntereses = Console.ReadLine();
            Console.Write("Nuevas Frases: ");
            string? nuevasFrases = Console.ReadLine();

            try
            {
                await _service.EditarUsuario(_usuarioLogueado!.Id, _usuarioLogueado.Nombre!,
                    nuevaEdad, nuevoGenero!, nuevaCarrera!, nuevosIntereses!, nuevasFrases!);
                Console.WriteLine("✅ Perfil actualizado exitosamente.");
                _usuarioLogueado = await _service.ObtenerUsuarioAsync(_usuarioLogueado.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        private async Task MostrarListaUsuariosAsync()
        {
            var todosUsuarios = await _service.ConsultarUsuarioAsync();
            Console.WriteLine("\n📋 Usuarios registrados:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("+-----+----------------------+");
            Console.WriteLine("| ID  | Nombre               |");
            Console.WriteLine("+-----+----------------------+");
            Console.ResetColor();

            foreach (var u in todosUsuarios)
            {
                if (u != null)
                    Console.WriteLine($"| {u.Id,-3} | {u.Nombre,-20} |");
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("+-----+----------------------+");
            Console.ResetColor();
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
