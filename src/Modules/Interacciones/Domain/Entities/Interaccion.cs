using System;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities
{
    public enum TipoInteraccion
    {
        LIKE,
        DISLIKE
    }

    public class Interaccion
    {
        public int Id { get; set; }
        public int IdUsuarioOrigen { get; set; }
        public int IdUsuarioDestino { get; set; }
        public TipoInteraccion TipoInteraccion { get; set; }  // Enum en vez de bool
        public DateTime FechaInteraccion { get; set; } = DateTime.Now;
    }
}
