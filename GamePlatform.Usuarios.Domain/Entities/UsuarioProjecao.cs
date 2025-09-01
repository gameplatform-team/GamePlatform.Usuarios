namespace GamePlatform.Usuarios.Domain.Entities;

public class UsuarioProjecao : BaseEntity
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; }
    public string Role { get; set; } // "Usuario" ou "Admin"
    public bool Ativo { get; set; } = true;
}
