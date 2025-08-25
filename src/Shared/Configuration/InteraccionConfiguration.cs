using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Interacciones.Domain.Entities;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campuslove_Ivanna_Sebastian.src.Shared.Configuration
{
    public class InteraccionConfiguration : IEntityTypeConfiguration<Interaccion>
    {
        public void Configure(EntityTypeBuilder<Interaccion> builder)
        {
           
            // Nombre de la tabla
            builder.ToTable("Interacciones");

            // Clave primaria
            builder.HasKey(i => i.Id);

            // Propiedades
            builder.Property(i => i.EsLike)
                   .IsRequired();

            builder.Property(i => i.Fecha)
                   .IsRequired();

            // Relaciones con Usuario (origen)
            builder.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(i => i.IdUsuarioOrigen)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relaciones con Usuario (destino)
            builder.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(i => i.IdUsuarioDestino)
                   .OnDelete(DeleteBehavior.Restrict);
        
        }
    }
}