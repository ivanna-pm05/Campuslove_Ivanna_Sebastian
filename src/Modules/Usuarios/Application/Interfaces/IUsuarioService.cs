using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task RegistrarUsuarioAsync(string nombre, string clave);
        Task<Usuario?> LoginAsync(string nombre, string clave);
        Task CompletarPerfilAsync(int id, int edad, string genero, string carrera, string intereses, string frases);
        Task EditarUsuario(int id, string NuevoNombre, int NuevaEdad, string NuevoGenero, string NuevaCarrera, string NuevoIntereses, string NuevaFrases);
        Task EliminarUsuario(int id);
        Task<Usuario?> ObtenerUsuarioAsync(int id);
        Task<IEnumerable<Usuario>> ConsultarUsuarioAsync();
        Task<Usuario?> ObtenerUsuarioPorNombreAsync(string nombre);

    }
}