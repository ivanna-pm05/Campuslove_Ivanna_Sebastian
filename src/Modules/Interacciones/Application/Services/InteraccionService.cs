using System.Collections.Generic;
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

        public async Task RegistrarLikeAsync(int idUsuarioOrigen, int idUsuarioDestino)
        {
            var interaccion = new Interaccion
            {
                IdUsuarioOrigen = idUsuarioOrigen,
                IdUsuarioDestino = idUsuarioDestino,
                TipoInteraccion = TipoInteraccion.LIKE
            };

            _repository.Add(interaccion);
            await _repository.SaveAsync();
        }

        public async Task RegistrarDislikeAsync(int idUsuarioOrigen, int idUsuarioDestino)
        {
            var interaccion = new Interaccion
            {
                IdUsuarioOrigen = idUsuarioOrigen,
                IdUsuarioDestino = idUsuarioDestino,
                TipoInteraccion = TipoInteraccion.DISLIKE
            };

            _repository.Add(interaccion);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<Interaccion?>> ObtenerInteraccionesDeUsuarioAsync(int idUsuario)
        {
            return await _repository.GetByUsuarioIdAsync(idUsuario);
        }

    }
}
