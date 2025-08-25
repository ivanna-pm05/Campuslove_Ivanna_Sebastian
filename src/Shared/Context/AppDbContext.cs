using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Campuslove_Ivanna_Sebastian.src.Shared.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Interaccion> Interacciones => Set<Interaccion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración específica para Interaccion
            modelBuilder.Entity<Interaccion>()
                .ToTable("interacciones");
            modelBuilder.Entity<Interaccion>()
                .Property(i => i.TipoInteraccion)
                .HasConversion(
                    v => v.ToString(), // enum a string
                    v => (TipoInteraccion)Enum.Parse(typeof(TipoInteraccion), v) // string a enum
                );

            // Aplicar otras configuraciones desde ensamblado
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
