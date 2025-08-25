namespace GamePlatform.Usuarios.Application.Interfaces.Services;

public interface IUsuarioContextService
{
    Guid GetUsuarioId();
    string GetRole();
    bool UsuarioEhAdmin();
}