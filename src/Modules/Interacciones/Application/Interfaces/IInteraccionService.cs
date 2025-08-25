using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Interfaces
{
    public interface IInteraccionService
    {
        void RegistrarInteraccion(int idUsuarioOrigen, int idUsuarioDestino, bool esLike);
        IEnumerable<Interaccion> ObtenerInteraccionesDeUsuario(int idUsuario);
        IEnumerable<Interaccion> ObtenerTodas();
    }
}