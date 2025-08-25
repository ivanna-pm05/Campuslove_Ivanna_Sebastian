using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities
{
    public class Interaccion
    {
        public int Id { get; set; }
        public int IdUsuarioOrigen { get; set; }
        public int IdUsuarioDestino { get; set; }
        public bool EsLike { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}