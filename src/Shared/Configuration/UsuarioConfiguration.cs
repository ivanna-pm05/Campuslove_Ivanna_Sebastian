using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Usuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campuslove_Ivanna_Sebastian.src.Shared.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Edad)
                   .IsRequired();

            builder.Property(u => u.Genero)
                   .HasMaxLength(20);

            builder.Property(u => u.Carrera)
                   .HasMaxLength(100);

            builder.Property(u => u.Intereces)
                   .HasMaxLength(200);

            builder.Property(u => u.Frases)
                   .HasMaxLength(250);
        }
    }
}