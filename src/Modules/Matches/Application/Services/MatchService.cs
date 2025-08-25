using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Matches.Application.Interfaces;
using Campuslove_Ivanna_Sebastian.src.Modules.Matches.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Matches.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _repository;

        public MatchService(IMatchRepository repository)
        {
            _repository = repository;
        }

        public void RegistrarMatch(int idUsuario1, int idUsuario2)
        {
            var match = new Match
            {
                IdUsuario1 = idUsuario1,
                IdUsuario2 = idUsuario2
            };

            _repository.Add(match);
        }

        public IEnumerable<Match> ObtenerMatchesDeUsuario(int idUsuario)
        {
            return _repository.GetByUsuario(idUsuario);
        }

        public IEnumerable<Match> ObtenerTodos()
        {
            return _repository.GetAll();
        }
    }
}