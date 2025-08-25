using System;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuariorepo;

        public UsuarioService(IUsuarioRepository usuariorepo)
        {
            _usuariorepo = usuariorepo;
        }

        public Task<IEnumerable<Usuario>> ConsultarJugadorAsync()
        {
            return _usuariorepo.GetAllAsync()!;
        }
        public async Task RegistrarUsuarioAsync(string nombre, string clave, int edad, string genero, string carrera, string intereses, string frases)
        {
            var existente = await _usuariorepo.GetAllAsync();
            if (existente.Any(u => u?.Nombre == nombre))
                throw new Exception("El usuario ya existe.");

            var usuario = new Usuario
            {
                Nombre = nombre,
                Clave = clave,
                Edad = edad,
                Genero = genero,
                Carrera = carrera,
                Intereses = intereses,
                Frases = frases
            };

            _usuariorepo.Add(usuario);
            await _usuariorepo.SaveAsync();
        }
        public async Task EditarUsuario(int id, string nuevoNombre, int nuevaEdad, string nuevoGenero, string nuevaCarrera, string nuevoIntereses, string nuevaFrases)
        {
            var usuario = await _usuariorepo.GetByIdAsync(id);
            if (usuario == null)
                throw new Exception($"Usuario con Id {id} no encontrado.");

            usuario.Nombre = nuevoNombre;
            usuario.Edad = nuevaEdad;
            usuario.Genero = nuevoGenero;
            usuario.Carrera = nuevaCarrera;
            usuario.Intereses = nuevoIntereses;
            usuario.Frases = nuevaFrases;

            _usuariorepo.Update(usuario);
            await _usuariorepo.SaveAsync();
        }

        public async Task EliminarUsuario(int id)
        {
            var usuario = await _usuariorepo.GetByIdAsync(id);
            if (usuario == null)
                throw new Exception("Usario con Id no encontardo.");
            _usuariorepo.Remove(usuario);
            await _usuariorepo.SaveAsync();
        }
        public async Task<Usuario?> ObtenerUsuarioAsync(int id)
        {
            return await _usuariorepo.GetByIdAsync(id);
        }
        public async Task<Usuario?> ObtenerUsuarioPorNombreAsync(string nombre)
        {
            return await _usuariorepo.GetByNombreAsync(nombre);
        }
    }
}