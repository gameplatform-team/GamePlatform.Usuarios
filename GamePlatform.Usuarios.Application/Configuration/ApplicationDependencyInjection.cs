using GamePlatform.Usuarios.Application.Interfaces.Services;
using GamePlatform.Usuarios.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GamePlatform.Usuarios.Application.Configuration;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();

        services.AddScoped<IUsuarioContextService, UsuarioContextService>();

        return services;
    }
}
