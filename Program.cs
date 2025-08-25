using System.ComponentModel.Design;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.UI;
using Campuslove_Ivanna_Sebastian.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

var context = DbContextFactory.Create();
bool salir = false;
while (salir)
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
    Console.WriteLine(" -> ");
    switch (opm)
    {
        case 1:
            await new MenuUsuarios(context).RenderMenu();
            break;
        case 2:
            await new MenuPerfiles(context).RenderMenu();
            break;
        case 3:
            await new MenuMaches(context).RenderMenu();
            break;
        case 4:
            await new MenuEstadisticas(context).RenderMenu();
            break;
        case 5:
            break;
    }
}