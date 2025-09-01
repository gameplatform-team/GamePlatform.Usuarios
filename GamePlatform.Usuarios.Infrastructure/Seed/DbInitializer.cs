using GamePlatform.Usuarios.Domain.Entities;
using GamePlatform.Usuarios.Domain.Events;
using GamePlatform.Usuarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace GamePlatform.Usuarios.Infrastructure.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(DataContext context)
    {
        await context.Database.MigrateAsync();

        if (!context.Usuarios.Any())
        {
            var admin = Usuario.Registrar(
                id: Guid.NewGuid(),
                nome: "Administrador",
                email: "admin@gameplatform.com",
                senhaHash: BCrypt.Net.BCrypt.HashPassword("123456")
            );

            admin.Promover();

            context.Usuarios.Add(new UsuarioProjecao
            {
                Id = admin.Id,
                Nome = admin.Nome,
                Email = admin.Email,
                Role = admin.Role,
                SenhaHash = admin.SenhaHash,
                Ativo = true
            });

            var evento = new Evento(
                aggregateId: admin.Id,
                versao: 1,
                tipoEvento: typeof(UsuarioRegistrado).AssemblyQualifiedName!,
                dados: System.Text.Json.JsonSerializer.Serialize(new UsuarioRegistrado(admin.Id, admin.Nome, admin.Email, admin.SenhaHash, true))
            );

            await context.Eventos.AddAsync(evento);

            await context.SaveChangesAsync();
        }
    }
}
