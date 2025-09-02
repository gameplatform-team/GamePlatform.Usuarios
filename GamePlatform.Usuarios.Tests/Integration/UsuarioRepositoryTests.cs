using FluentAssertions;
using GamePlatform.Usuarios.Domain.Entities;
using GamePlatform.Usuarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Usuarios.Tests.Integration;

public class UsuarioRepositoryTests
{
    private DataContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new DataContext(options);
    }

    [Fact]
    public async Task Deve_Salvar_Usuario_No_Banco()
    {
        using var context = CriarContexto();

        var usuario = new UsuarioProjecao
        {
            Id = Guid.NewGuid(),
            Nome = "Admin",
            Email = "admin@test.com",
            SenhaHash = "hash123",
            Role = "Admin",
            Ativo = true
        };

        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        var salvo = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == "admin@test.com");

        salvo.Should().NotBeNull();
        salvo!.Nome.Should().Be("Admin");
        salvo.Ativo.Should().BeTrue();
    }
}
