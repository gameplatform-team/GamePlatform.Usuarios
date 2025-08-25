using System.ComponentModel.DataAnnotations;

namespace GamePlatform.Usuarios.Application.DTOs;

public class RegistrarUsuarioDto
{
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }
}
