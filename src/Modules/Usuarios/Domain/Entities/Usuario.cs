using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string? Genero { get; set; }
        public string? Carrera { get; set; }
        public string? Intereces { get; set; }
        public string? Frases { get; set; }
    }
}