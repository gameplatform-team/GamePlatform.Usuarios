using GamePlatform.Usuarios.Application.DTOs;
using GamePlatform.Usuarios.Application.DTOs.Usuario;

namespace GamePlatform.Usuarios.Application.Interfaces.Services;

public interface IUsuarioService
{
    Task<UsuarioDto?> ObterPorIdAsync(Guid id);
    Task<BaseResponseDto> AtualizarAsync(Guid id, AtualizarUsuarioDto dto);
    Task<BaseResponseDto> ExcluirAsync(Guid id);
    Task<IEnumerable<UsuarioDto>> ListarTodosAsync();
    Task<BaseResponseDto> RegistrarAsync(RegistrarUsuarioDto dto);
    Task<(bool sucesso, string? token, string mensagem)> LoginAsync(LoginDto dto);
}
