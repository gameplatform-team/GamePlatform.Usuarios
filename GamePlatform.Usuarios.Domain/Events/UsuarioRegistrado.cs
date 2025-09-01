namespace GamePlatform.Usuarios.Domain.Events;

public record UsuarioRegistrado(Guid UsuarioId, string Nome, string Email, string senha, bool Ativo);