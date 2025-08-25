using System.Collections.Generic;
using System.Linq;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        // Crear usuario
        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        // Editar usuario
        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        // Eliminar usuario
        public void Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }

        // Buscar usuario por Id
        public Usuario GetById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        // Obtener todos los usuarios
        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }
    }
}