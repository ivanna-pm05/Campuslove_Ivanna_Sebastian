using System.Collections.Generic;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Interfaces
{
    public interface IInteraccionRepository
    {
        void Add(Interaccion interaccion);
        Task SaveAsync();
        Task<IEnumerable<Interaccion?>> GetAllAsync();
        Task<IEnumerable<Interaccion?>>GetByUsuarioIdAsync(int idUsuario);
    }
}
