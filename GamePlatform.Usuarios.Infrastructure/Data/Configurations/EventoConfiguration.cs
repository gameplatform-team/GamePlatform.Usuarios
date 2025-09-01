using GamePlatform.Usuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlatform.Usuarios.Infrastructure.Data.Configurations;

public class EventoConfiguration : IEntityTypeConfiguration<Evento>
{
    public void Configure(EntityTypeBuilder<Evento> builder)
    {
        builder.ToTable("Evento");

        builder.HasKey(u => u.Id);

        builder.Property(e => e.TipoEvento)
            .IsRequired();

        builder.Property(e => e.Dados)
            .IsRequired();

        builder.HasIndex(e => new { e.AggregateId, e.Versao })
            .IsUnique();

        builder.Property(e => e.CreatedAt)
           .IsRequired()
           .HasDefaultValueSql("CURRENT_TIMESTAMP"); 
    }
}
