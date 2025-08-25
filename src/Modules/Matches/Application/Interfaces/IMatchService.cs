using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Matches.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Matches.Application.Interfaces
{
    public interface IMatchService
    {
        void RegistrarMatch(int idUsuario1, int idUsuario2);
        IEnumerable<Match> ObtenerMatchesDeUsuario(int idUsuario);
        IEnumerable<Match> ObtenerTodos();
    }
}