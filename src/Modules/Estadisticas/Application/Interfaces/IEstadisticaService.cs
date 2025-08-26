using System.Collections.Generic;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Estadisticas.Domain;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Estadisticas.Application.Interfaces
{
    public interface IEstadisticaService
    {
        Task<IEnumerable<ReporteEstadistica>> ObtenerEstadisticasAsync();
    }
}
