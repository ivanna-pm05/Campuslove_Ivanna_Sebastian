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
        readonly UsuarioRepository _usuariorepo = null!;
        readonly UsuarioService service = null!;

        public MenuUsuarios(AppDbContext context)
        {
            _context = context;
            _usuariorepo = new UsuarioRepository(_context);
            service = new UsuarioService(_usuariorepo);
        }
        public async Task RenderMenu()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("+===============================+");
                Console.WriteLine("|        MENÚ DE USUARIOS       |");
                Console.WriteLine("+===============================+");
                Console.WriteLine("| 1. Registrar Usuario          |");
                Console.WriteLine("| 2. Iniciar Sesion             |");
                Console.WriteLine("| 3. Editar Usuario             |");
                Console.WriteLine("| 4. Eliminar Usuario           |");
                Console.WriteLine("| 5. Buscar Usuario             |");
                Console.WriteLine("| 6. Regresar al menú principal |");
                Console.WriteLine("+===============================+");
                Console.Write("Seleccione una opción: ");

                int opcion = LeerEntero("-> ");

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("+=====================+");
                        Console.WriteLine("|  Registrar Usuario  |");
                        Console.WriteLine("+=====================+");
                        Console.Write("Nombre Usuario: ");
                        string? nombre = Console.ReadLine();
                        int edad = LeerEntero("Edad Usuario");
                        Console.WriteLine("Ingrese la contraseña (letras y / o números):");
                        string? clave = Console.ReadLine();
                        Console.Write("Genero: ");
                        string? genero = Console.ReadLine();
                        Console.Write("Carrera: ");
                        string? carrera = Console.ReadLine();
                        Console.Write("Intereces: ");
                        string? intereses = Console.ReadLine();
                        Console.Write("Frases: ");
                        string? frases = Console.ReadLine();
                        await service.RegistrarUsuarioAsync(nombre!, clave!, edad!, genero!, carrera!, intereses!, frases!);
                        Console.Write("✅ Usuario Registrado.");
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("╔════════════════════════════════════════════╗");
                        Console.WriteLine("║               Iniciar Sesion               ║");
                        Console.WriteLine("╠════════════════════════════════════════════╣");
                        Console.WriteLine("║          Ingrese el nombre del usuario     ║");
                        Console.WriteLine("╚════════════════════════════════════════════╝");
                        string nombre2 = Console.ReadLine()!;

                        if (string.IsNullOrWhiteSpace(nombre2))
                        {
                            Console.WriteLine("⚠️ El nombre de usuario no puede estar vacío.");
                            Console.ReadKey();
                            break;
                        }

                        Usuario? usuario = await service.ObtenerUsuarioPorNombreAsync(nombre2);

                        if (usuario == null)
                        {
                            Console.WriteLine("❌ Usuario no encontrado.");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Ingrese la contraseña: ");
                        string? claveIngresada = Console.ReadLine();

                        if (usuario.Clave == claveIngresada)
                        {
                            Sesion.IniciarSesion(usuario); // 👉 Guardamos al usuario en sesión
                            Console.WriteLine($"✅ Bienvenido, {usuario.Nombre}.");
                            Console.WriteLine("✅ Inicio de sesión exitoso.");
                        }
                        else
                        {
                            Console.WriteLine("❌ Contraseña incorrecta.");
                        }

                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("+==================+");
                        Console.WriteLine("|  Editar Usuario  |");
                        Console.WriteLine("+==================+");
                        int idUp = LeerEntero("ID a editar: ");
                        Console.Write("Nuevo Usuario: ");
                        string? nuevoName = Console.ReadLine();
                        int NuevaEdad = LeerEntero("Nueva edad: ");
                        Console.Write("Nuevo Genero (F/M/O): ");
                        string? NuevoGenero = Console.ReadLine();
                        Console.Write("Nueva Carrera: ");
                        string? NuevaCarrera = Console.ReadLine();
                        Console.Write("Nuevos Intereces: ");
                        string? NuevoIntereces = Console.ReadLine();
                        Console.Write("Nuevas Frases: ");
                        string? NuevaFrases = Console.ReadLine();
                        await service.EditarUsuario(idUp, nuevoName!, NuevaEdad!, NuevoGenero!, NuevaCarrera!, NuevoIntereces!, NuevaFrases!);
                        Console.WriteLine("✏️ Editado.");
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("+=====================+");
                        Console.WriteLine("|  Eliminar Usuario   |");
                        Console.WriteLine("+=====================+");
                        int idDel = LeerEntero("ID del Usuario a eliminar: ");
                        await service.EliminarUsuario(idDel);
                        Console.WriteLine("🗑️ Usuario Eliminado.");
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("+=====================+");
                        Console.WriteLine("|    Buscar Usuario   |");
                        Console.WriteLine("+=====================+");
                        int id = LeerEntero("ID del Usuario a buscar: ");
                        Usuario? usuarioEncontado = await service.ObtenerUsuarioAsync(id);
                        if (usuarioEncontado != null)
                            Console.WriteLine($"👤 {usuarioEncontado.Nombre} - {usuarioEncontado.Edad} - {usuarioEncontado.Genero} - {usuarioEncontado.Carrera} - {usuarioEncontado.Intereses} - {usuarioEncontado.Frases}");
                        else
                            Console.WriteLine("❌ Usuario no encontrado.");
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 6:
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("⚠ Opción inválida, presione una tecla para continuar...");
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