using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GamePlatform.Usuarios.Infrastructure.Data;
using GamePlatform.Usuarios.Domain.Interfaces;
using GamePlatform.Usuarios.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace GamePlatform.Usuarios.Application.Configuration;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}