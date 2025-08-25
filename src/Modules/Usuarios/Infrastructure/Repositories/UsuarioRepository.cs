using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<Usuario?> GetByNombreAsync(string nombre)
        {
            var n = (nombre ?? string.Empty).Trim();
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == n);
        }
        public async Task<bool> ExistsByNombreAsync(string nombre)
        {
            var n = (nombre ?? string.Empty).Trim();
            return await _context.Usuarios.AnyAsync(u => u.Nombre == n);
        }
        public async Task<IEnumerable<Usuario?>> GetAllAsync() =>
            await _context.Usuarios.ToListAsync();

        public void Add(Usuario usuario) =>
            _context.Usuarios.Add(usuario);

        public void Remove(Usuario usuario) =>
            _context.Usuarios.Remove(usuario);
        public void Update(Usuario usuario) =>
            _context.SaveChanges();
        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
        public async Task<Usuario?> ObtenerUsuarioPorNombreAsync(string nombre)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == nombre);
        }
    }
}