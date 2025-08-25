using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Infrastructure.Repositories
{
    public class InteraccionRepository : IInteraccionRepository
    {
        private readonly List<Interaccion> _interacciones = new();
        private int _nextId = 1;

        public void Add(Interaccion interaccion)
        {
            interaccion.Id = _nextId++;
            _interacciones.Add(interaccion);
        }

        public IEnumerable<Interaccion> GetAll() => _interacciones;

        public IEnumerable<Interaccion> GetByUsuario(int idUsuario)
            => _interacciones.Where(i => i.IdUsuarioOrigen == idUsuario || i.IdUsuarioDestino == idUsuario);
    }
}