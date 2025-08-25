using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Matches.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Matches.Infrastructure.Repositories
{
    public class MatchRepository
    {
        private readonly List<Match> _matches = new();
        private int _nextId = 1;

        public void Add(Match match)
        {
            match.Id = _nextId++;
            _matches.Add(match);
        }

        public IEnumerable<Match> GetAll() => _matches;

        public IEnumerable<Match> GetByUsuario(int idUsuario)
            => _matches.Where(m => m.IdUsuario1 == idUsuario || m.IdUsuario2 == idUsuario);
    }
}