using System.Collections.Generic;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Interfaces
{
    public interface IInteraccionService
    {
        Task RegistrarLikeAsync(int idUsuarioOrigen, int idUsuarioDestino);
        Task RegistrarDislikeAsync(int idUsuarioOrigen, int idUsuarioDestino);
        Task<IEnumerable<Interaccion?>> ObtenerInteraccionesDeUsuarioAsync(int idUsuario);
    }
}
