namespace GamePlatform.Usuarios.Domain.Events;

public record UsuarioAtualizado(Guid UsuarioId, string Nome, string Email, string NovaSenha, string Role);