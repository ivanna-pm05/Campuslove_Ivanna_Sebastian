using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Matches.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Matches.Application.Interfaces
{
    public interface IMatchRepository
    {
        void Add(Match match);
        IEnumerable<Match> GetAll();
        IEnumerable<Match> GetByUsuario(int idUsuario);
    }
}