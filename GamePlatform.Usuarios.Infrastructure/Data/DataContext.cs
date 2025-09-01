using GamePlatform.Usuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Usuarios.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<UsuarioProjecao> Usuarios { get; set; }
    public DbSet<Evento> Eventos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
