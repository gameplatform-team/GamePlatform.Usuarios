using GamePlatform.Usuarios.Domain.Entities;
using GamePlatform.Usuarios.Domain.Interfaces;
using GamePlatform.Usuarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Usuarios.Infrastructure.Repositories;

public class UsuarioLeituraRepository(DataContext context) : IUsuarioLeituraRepository
{
    private readonly DataContext _context = context;

    public async Task<UsuarioProjecao?> ObterPorIdAsync(Guid id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<UsuarioProjecao?> ObterPorEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<UsuarioProjecao>> ListarTodosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email);
    }
  
    public async Task<bool> EmailJaExisteAsync(string email, Guid? id = null)
    {
        return await _context.Usuarios
            .AnyAsync(u => u.Email == email && (id == null || u.Id != id));
    }
}
