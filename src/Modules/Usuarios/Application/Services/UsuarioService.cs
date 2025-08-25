using System;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuariorepo;

        public UsuarioService(IUsuarioRepository usuariorepo)
        {
            _usuariorepo = usuariorepo;
        }

        // ✅ Registrar un nuevo usuario
        public async Task RegistrarUsuarioAsync(string nombre, int edad, string genero, string carrera, string intereces, string frases)
        {
            var existente = await _usuariorepo.GetAllAsync();
            if (existente.Any(u => u.Nombre == nombre))
                throw new Exception("El usuario ya existe.");

            var usuario = new Usuario
            {
                Nombre = nombre,
                Edad = edad,
                Genero = genero,
                Carrera = carrera,
                Intereces = intereces,
                Frases = frases
            };

            await _usuariorepo.AddAsync(usuario);
            await _usuariorepo.SaveAsync();
        }
// ✅ Editar usuario existente
        public async Task EditarUsuario(int id, string nuevoNombre, int nuevaEdad, string nuevoGenero, string nuevaCarrera, string nuevoIntereces, string nuevaFrases)
        {
            var usuario = await _usuariorepo.GetByIdAsync(id);
            if (usuario == null)
                throw new Exception($"Usuario con Id {id} no encontrado.");

            usuario.Nombre = nuevoNombre;
            usuario.Edad = nuevaEdad;
            usuario.Genero = nuevoGenero;
            usuario.Carrera = nuevaCarrera;
            usuario.Intereces = nuevoIntereces;
            usuario.Frases = nuevaFrases;

            _usuariorepo.Update(usuario);
            await _usuariorepo.SaveAsync();
        }

        // ✅ Eliminar usuario por Id
        public async Task EliminarUsuario(int id)
        {
            var usuario = await _usuariorepo.GetByIdAsync(id);
            if (usuario == null)
                throw new Exception($"Usuario con Id {id} no encontrado.");

            _usuariorepo.Remove(usuario);
            await _usuariorepo.SaveAsync();
        }

        // ✅ Obtener un usuario por Id
        public async Task<Usuario?> ObtenerUsuarioAsync(int id)
        {
            return await _usuariorepo.GetByIdAsync(id);
        }

        // ✅ Obtener todos los usuarios
        public async Task<System.Collections.Generic.IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            return await _usuariorepo.GetAllAsync();
        }
    }
}