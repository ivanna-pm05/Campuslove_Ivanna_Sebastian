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
        private Usuario? _usuarioLogueado = null;

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
                Console.WriteLine("| 3. Ingresar Datos Usuario     |");
                Console.WriteLine("| 4. Eliminar Usuario           |");
                Console.WriteLine("| 5. Buscar Usuario             |");
                Console.WriteLine("| 6. Editar Usuario             |");
                Console.WriteLine("| 7. Regresar al menú principal |");
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
                            await service.RegistrarUsuarioAsync(nombre, clave);
                            Console.WriteLine("✅ Usuario registrado. Ahora inicia sesión para completar tu perfil.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Error: {ex.Message}");
                        }
                        
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
        
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("╔════════════════════════════════════════════╗");
                        Console.WriteLine("║               Iniciar Sesión               ║");
                        Console.WriteLine("╠════════════════════════════════════════════╣");
                        
                        Console.Write("Nombre de usuario: ");
                        string nombre2 = Console.ReadLine()!;

                        Console.Write("Contraseña: ");
                        string clave2 = Console.ReadLine()!;

                        if (string.IsNullOrWhiteSpace(nombre2) || string.IsNullOrWhiteSpace(clave2))
                        {
                            Console.WriteLine("❌ Nombre y contraseña son obligatorios.");
                            Console.ReadKey();
                            return;
                        }

                        try
                        {
                            var usuario = await service.LoginAsync(nombre2, clave2);
                            if (usuario != null)
                            {
                                _usuarioLogueado = usuario;
                                Console.WriteLine($"✅ Bienvenido, {usuario.Nombre}.");
                                
                                if (!usuario.PerfilCompleto)
                                {
                                    Console.WriteLine("⚠ Tu perfil está incompleto. Ve a 'Completar Perfil'.");
                                }
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
                        break;

                    case 3:
                        if ( _usuarioLogueado == null) return;

                        Console.Clear();
                        Console.WriteLine("+=======================+");
                        Console.WriteLine("|      Datos Perfil     |");
                        Console.WriteLine("+=======================+");
                        
                        Console.Write("Edad: ");
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
                            await service.CompletarPerfilAsync(_usuarioLogueado.Id, edad, genero!, carrera!, intereses!, frases!);
                            Console.WriteLine("✅ Perfil completado exitosamente.");
                            
                            // Actualizar usuario en sesión
                            _usuarioLogueado = await service.ObtenerUsuarioAsync(_usuarioLogueado.Id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Error: {ex.Message}");
                        }
                        
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
                        if (_usuarioLogueado == null) return;

                        Console.Clear();
                        Console.WriteLine("+==================+");
                        Console.WriteLine("|   Editar Perfil  |");
                        Console.WriteLine("+==================+");
                        
                        Console.Write("Nueva Edad: ");
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
                            await service.EditarUsuario(_usuarioLogueado.Id, _usuarioLogueado.Nombre!, 
                                nuevaEdad, nuevoGenero!, nuevaCarrera!, nuevosIntereses!, nuevasFrases!);
                            Console.WriteLine("✅ Perfil actualizado exitosamente.");
                            
                            // Actualizar usuario en sesión
                            _usuarioLogueado = await service.ObtenerUsuarioAsync(_usuarioLogueado.Id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Error: {ex.Message}");
                        }
                        
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
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