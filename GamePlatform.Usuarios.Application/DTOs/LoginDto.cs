using System.ComponentModel.DataAnnotations;

namespace GamePlatform.Usuarios.Application.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }
}
