using GamePlatform.Usuarios.Domain.Entities;

namespace GamePlatform.Usuarios.Domain.Interfaces;

public interface IUsuarioLeituraRepository
{
    Task<UsuarioProjecao?> ObterPorIdAsync(Guid id);

    Task<UsuarioProjecao?> ObterPorEmailAsync(string email);

    Task<IEnumerable<UsuarioProjecao>> ListarTodosAsync();

    Task<bool> ExisteEmailAsync(string email);

    Task<bool> EmailJaExisteAsync(string email, Guid? id = null);
}