﻿using GamePlatform.Usuarios.Domain.Entities;
using GamePlatform.Usuarios.Domain.Interfaces;
using GamePlatform.Usuarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Usuarios.Infrastructure.Repositories;

public class UsuarioRepository(DataContext context) : IUsuarioRepository
{
    private readonly DataContext _context = context;

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public void Remover(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
    }

    public async Task<IEnumerable<Usuario>> ListarTodosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<bool> ExisteEmailAsync(string email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email);
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }

    public async Task SalvarAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailJaExisteAsync(string email, Guid? id = null)
    {
        return await _context.Usuarios
            .AnyAsync(u => u.Email == email && (id == null || u.Id != id));
    }
}
