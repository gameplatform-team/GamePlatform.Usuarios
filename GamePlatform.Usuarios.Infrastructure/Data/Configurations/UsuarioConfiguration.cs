using GamePlatform.Usuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePlatform.Usuarios.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<UsuarioProjecao>
{
    public void Configure(EntityTypeBuilder<UsuarioProjecao> builder)
    {
        builder.ToTable("Usuario");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(200); 

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.SenhaHash)
            .IsRequired()
            .HasMaxLength(500); 

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(20);  

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.CreatedAt)
          .IsRequired()
          .HasColumnName("CreatedAt")
          .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("UpdatedAt");

        builder.Property(u => u.Ativo)
               .IsRequired()
               .HasDefaultValue(true);
    }
}
