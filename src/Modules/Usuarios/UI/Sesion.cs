using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.UI
{
    public class Sesion
    {
        public static Usuario? UsuarioActual { get; private set; }
        public static bool IsLoggedIn => UsuarioActual != null;

        public static void IniciarSesion(Usuario usuario)
        {
            UsuarioActual = usuario;
        }

        public static void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}