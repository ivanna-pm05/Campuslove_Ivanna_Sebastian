using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities;
using Campuslove_Ivanna_Sebastian.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Infrastructure.Repositories
{
    public class InteraccionRepository : IInteraccionRepository
    {
        private readonly AppDbContext _context;

        public InteraccionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Interaccion interaccion) =>
            _context.Interacciones.Add(interaccion);

        public async Task<IEnumerable<Interaccion?>> GetAllAsync() =>
            await _context.Interacciones.ToListAsync();

        public async Task<IEnumerable<Interaccion?>> GetByUsuarioIdAsync(int idUsuario)
        {
            return await _context.Interacciones
                .Where(i => i.IdUsuarioOrigen == idUsuario || i.IdUsuarioDestino == idUsuario)
                .ToListAsync();
        }

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
