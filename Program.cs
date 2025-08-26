using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.UI;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.UI;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Shared.Helpers;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;

var context = DbContextFactory.Create();
bool salir = false;
int idUsuarioLogueado = 0; // Usuario logueado actual

while (!salir)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("+========================================+");
    Console.WriteLine("|        Menu Usuario                    |");
    Console.WriteLine("+========================================+");
    Console.WriteLine("| 1. Registrar Como Nuevo Usuario        |");
    Console.WriteLine("| 2. Ver Perfiles y Dar Like y Dislike   |");
    Console.WriteLine("| 3. Ver Mis Coincidencias (Matches)     |");
    Console.WriteLine("| 4. Ver estadísticas del sistema        |");
    Console.WriteLine("| 5. Salir                               |");
    Console.WriteLine("+========================================+");
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine("Seleccione Una Opcion");
    int opm = LeerEntero("-> ");

    switch (opm)
    {
        case 1: // Registro de usuario
            await new MenuUsuarios(context).RenderMenu();
            break;

        case 2: // Interacciones
            var usuarioRepo = new UsuarioRepository(context);
            var usuarioService = new UsuarioService(usuarioRepo);
            var todosUsuarios = await usuarioService.ConsultarUsuarioAsync();

            if (!todosUsuarios.Any())
            {
                Console.WriteLine("⚠ No hay usuarios registrados. Debe crear una cuenta primero.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Console.WriteLine("\n📋 Usuarios registrados:");
            Console.WriteLine("+-----+----------------------+");
            Console.WriteLine("| ID  | Nombre               |");
            Console.WriteLine("+-----+----------------------+");
            foreach (var usuario in todosUsuarios)
            {
                if (usuario != null)
                    Console.WriteLine($"| {usuario.Id,-3} | {usuario.Nombre,-20} |");
            }
            Console.WriteLine("+-----+----------------------+");

            int idSeleccionado = LeerEntero("Ingrese su ID de usuario: ");
            var usuarioSeleccionado = await usuarioService.ObtenerUsuarioAsync(idSeleccionado);
            if (usuarioSeleccionado == null)
            {
                Console.WriteLine("❌ ID inválido. No existe un usuario con ese ID.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Console.Write("Ingrese la contraseña: ");
            string clave = Console.ReadLine()!;
            if (usuarioSeleccionado.Clave != clave)
            {
                Console.WriteLine("❌ Contraseña incorrecta.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            idUsuarioLogueado = idSeleccionado; // Guardar usuario logueado
            await new MenuInteracciones(context).RenderMenu(idUsuarioLogueado);
            break;

        case 3: // Matches
            if (idUsuarioLogueado <= 0)
            {
                Console.WriteLine("⚠ Debes iniciar sesión primero para ver tus matches.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            await new MenuMatches(context).RenderMenu(idUsuarioLogueado);
            break;

        case 4: // Estadísticas
            await new MenuEstadisticas(context).RenderMenu(idUsuarioLogueado);
            break;

        case 5: // Salir
            salir = true;
            break;

        default:
            Console.WriteLine("❗ Opción inválida.");
            Console.ReadKey();
            break;
    }
}

int LeerEntero(string mensaje)
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
