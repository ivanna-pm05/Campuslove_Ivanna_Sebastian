using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;

namespace Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario?>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        void Add(Usuario usuario);
        void Update(Usuario usuario);
        void Remove(Usuario usuario);
        Task<bool> ExistsByNombreAsync(string nombre);
        Task<Usuario?> GetByNombreAsync(string nombre);
        Task SaveAsync();
    }
}