using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Matches.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int IdUsuario1 { get; set; }
        public int IdUsuario2 { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}