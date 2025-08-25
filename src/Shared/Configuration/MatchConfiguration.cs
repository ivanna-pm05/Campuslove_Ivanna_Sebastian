using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Campuslove_Ivanna_Sebastian.src.Modules.Matches.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Campuslove_Ivanna_Sebastian.src.Shared.Configuration
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("matches");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.IdUsuario1)
                   .IsRequired();

            builder.Property(m => m.IdUsuario2)
                   .IsRequired();

            builder.Property(m => m.Fecha)
                   .IsRequired();
        }
    }
}