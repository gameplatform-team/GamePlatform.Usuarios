using GamePlatform.Usuarios.Application.DTOs;

namespace GamePlatform.Usuarios.Application.Interfaces.Repositories;

public interface IUsuarioService
{
    Task<(bool sucesso, string mensagem)> RegistrarAsync(RegistrarUsuarioDto dto);
    Task<(bool sucesso, string? token, string mensagem)> LoginAsync(LoginDto dto);
}
