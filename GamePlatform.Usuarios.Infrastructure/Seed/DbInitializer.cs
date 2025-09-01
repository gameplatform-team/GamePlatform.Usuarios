using GamePlatform.Usuarios.Domain.Entities;
using GamePlatform.Usuarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Usuarios.Infrastructure.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(DataContext context)
    {
        await context.Database.MigrateAsync();

        if (!context.Usuarios.Any())
        {
            //var admin = new UsuarioProjecao( 
            //    nome: "Administrador",
            //    email: "admin@gameplatform.com",
            //    senhaHash: BCrypt.Net.BCrypt.HashPassword("123456"),
            //    role: "Admin"
            //);

            //context.Usuarios.Add(admin);
            //await context.SaveChangesAsync();
        }
    }
}
