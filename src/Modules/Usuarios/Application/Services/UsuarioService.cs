using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Interfaces;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuariorepo;
        public UsuarioService(IUsuarioRepository usuariorepo)
        {
            _usuariorepo = usuariorepo;
        }
        public async RegistrarUsuarioAsync(string nombre, int edad, string genero, string carrera, string intereces, string frases)
        {
            
        }
    }
}