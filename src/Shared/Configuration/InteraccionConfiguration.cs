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
            builder.ToTable("interacciones");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.TipoInteraccion)
                   .IsRequired()
                   .HasConversion(
                       v => v.ToString(),
                       v => (TipoInteraccion)Enum.Parse(typeof(TipoInteraccion), v)
                   );

            builder.Property(i => i.FechaInteraccion)
                   .IsRequired();

            builder.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(i => i.IdUsuarioOrigen)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(i => i.IdUsuarioDestino)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
