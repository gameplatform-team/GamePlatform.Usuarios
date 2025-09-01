using GamePlatform.Usuarios.Domain.Entities;

namespace GamePlatform.Usuarios.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorIdAsync(Guid id);
    Task SalvarAsync(Usuario usuario, int versaoEsperada);
    Task<int> ObterVersaoAsync(Guid id);
}
