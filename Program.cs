using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.UI;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.UI;
using Campuslove_Ivanna_Sebastian.src.Shared.Helpers;


var context = DbContextFactory.Create();

bool salir = false;
while (!salir)
{
    Console.Clear();
    Console.WriteLine("+========================================+");
    Console.WriteLine("|        Menu Usuario                    |");
    Console.WriteLine("+========================================+");
    Console.WriteLine("| 1. Registar Como Nuevo Usuario         |");
    Console.WriteLine("| 2. Ver Perfiles y Dar Like y Dislike   |");
    Console.WriteLine("| 3. Ver Mis Concidencias(Matches)       |");
    Console.WriteLine("| 4. Ver estadísticas del sistema        |");
    Console.WriteLine("| 5. Salir                               |");
    Console.WriteLine("+========================================+");
    Console.WriteLine();
    Console.WriteLine("Seleccione Una Opcion");
    int opm = LeerEntero("-> ");
    switch (opm)
    {
        case 1:
            await new MenuUsuarios(context).RenderMenu();
            break;
        case 2:
            int idUsuarioLogueado = LeerEntero("Ingrese su ID de usuario para continuar: ");
            await new MenuInteracciones(context).RenderMenu(idUsuarioLogueado);
            break;
           
        case 3:
            //await new MenuMaches(context).RenderMenu();
            break;
        case 4:
            //  await new MenuEstadisticas(context).RenderMenu();
            break;
        case 5:
            salir = true;
            break;
        default:
        Console.WriteLine("❗ Opción inválida.");
        break;
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
}