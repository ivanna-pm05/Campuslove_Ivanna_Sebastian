using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Services
{
    public class InteraccionService : IInteraccionService
    {
        private readonly IInteraccionRepository _repository;

        public InteraccionService(IInteraccionRepository repository)
        {
            _repository = repository;
        }

        public void RegistrarInteraccion(int idUsuarioOrigen, int idUsuarioDestino, bool esLike)
        {
            var interaccion = new Interaccion
            {
                IdUsuarioOrigen = idUsuarioOrigen,
                IdUsuarioDestino = idUsuarioDestino,
                EsLike = esLike
            };

            _repository.Add(interaccion);
        }

        public IEnumerable<Interaccion> ObtenerInteraccionesDeUsuario(int idUsuario)
        {
            return _repository.GetByUsuario(idUsuario);
        }

        public IEnumerable<Interaccion> ObtenerTodas()
        {
            return _repository.GetAll();
        }
    }
}