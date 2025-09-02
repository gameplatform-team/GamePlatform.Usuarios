using FluentAssertions;
using GamePlatform.Usuarios.Domain.Entities;

namespace GamePlatform.Usuarios.Tests.Unit;

public class UsuarioTests
{
    [Fact]
    public void Deve_Criar_Usuario_Com_Dados_Validos()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Rafael";
        var email = "rafael@test.com";
        var senhaHash = "hash123";

        // Act
        var usuario = Usuario.Registrar(id, nome, email, senhaHash);

        // Assert
        usuario.Should().NotBeNull();
        usuario.Nome.Should().Be(nome);
        usuario.Email.Should().Be(email);
        usuario.Ativo.Should().BeTrue();
        usuario.Role.Should().Be("Usuario");
    }
}