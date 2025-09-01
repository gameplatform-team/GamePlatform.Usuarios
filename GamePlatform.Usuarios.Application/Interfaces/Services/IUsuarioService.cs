using GamePlatform.Usuarios.Application.DTOs;
using GamePlatform.Usuarios.Application.DTOs.Usuario;

namespace GamePlatform.Usuarios.Application.Interfaces.Services;

public interface IUsuarioService
{
    Task<UsuarioDto?> ObterPorIdAsync(Guid id);
    Task<BaseResponseDto> RegistrarAsync(RegistrarUsuarioDto dto);
    Task<(bool sucesso, string? token, string mensagem)> LoginAsync(LoginDto dto);
    Task PromoverAsync(Guid id);
    Task<IEnumerable<UsuarioDto>> ObterTodosAsync();
    Task<BaseResponseDto> AtualizarAsync(Guid id, AtualizarUsuarioDto dto);
    Task<BaseResponseDto> ExcluirAsync(Guid id);
}
