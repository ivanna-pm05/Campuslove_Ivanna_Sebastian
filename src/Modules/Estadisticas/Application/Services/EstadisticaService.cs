using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Estadisticas.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Estadisticas.Domain;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Interfaces;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Estadisticas.Application.Services
{
    public class EstadisticaService : IEstadisticaService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IInteraccionService _interaccionService;

        public EstadisticaService(IUsuarioService usuarioService, IInteraccionService interaccionService)
        {
            _usuarioService = usuarioService;
            _interaccionService = interaccionService;
        }

        public async Task<IEnumerable<ReporteEstadistica>> ObtenerEstadisticasAsync()
        {
            var usuarios = (await _usuarioService.ConsultarUsuarioAsync()).Where(u => u != null).ToList();
            var interacciones = new List<Interaccion>();

            foreach (var u in usuarios)
            {
                var inters = await _interaccionService.ObtenerInteraccionesDeUsuarioAsync(u.Id);
                interacciones.AddRange(inters);
            }

            var estadisticas = usuarios.Select(u => new ReporteEstadistica
            {
                NombreUsuario = u.Nombre,
                LikesRecibidos = interacciones.Count(i => i.IdUsuarioDestino == u.Id && i.TipoInteraccion == TipoInteraccion.LIKE),
                LikesDados = interacciones.Count(i => i.IdUsuarioOrigen == u.Id && i.TipoInteraccion == TipoInteraccion.LIKE),
                Matches = interacciones.Count(i => 
                    i.IdUsuarioOrigen == u.Id && 
                    i.TipoInteraccion == TipoInteraccion.LIKE &&
                    interacciones.Any(j =>
                        j.IdUsuarioOrigen == i.IdUsuarioDestino &&
                        j.IdUsuarioDestino == u.Id &&
                        j.TipoInteraccion == TipoInteraccion.LIKE))
            }).ToList();

            return estadisticas;
        }
    }
}
