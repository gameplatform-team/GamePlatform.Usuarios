using GamePlatform.Usuarios.Domain.Entities;
using GamePlatform.Usuarios.Domain.Events;
using GamePlatform.Usuarios.Domain.Interfaces;
using GamePlatform.Usuarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GamePlatform.Usuarios.Infrastructure.Repositories;

public class UsuarioRepository(DataContext context) : IUsuarioRepository
{
    private readonly DataContext _context = context;

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        var eventos = await _context.Eventos
            .Where(e => e.AggregateId == id)
            .OrderBy(e => e.Versao)
            .ToListAsync();

        if (!eventos.Any()) return null;

        var eventosDominio = new List<object>();

        foreach (var e in eventos)
        {
            var tipo = Type.GetType(e.TipoEvento);
            if (tipo == null)
            {
                throw new Exception($"Tipo de evento não encontrado: {e.TipoEvento}");
            }

            var evento = JsonSerializer.Deserialize(e.Dados, tipo);
            if (evento != null)
                eventosDominio.Add(evento);
        }

        return Usuario.Reconstituir(eventosDominio);
    }


    public async Task SalvarAsync(Usuario usuario, int versaoEsperada)
    {
        var eventos = usuario.Changes
            .Select((e, i) =>
                new Evento(
                    usuario.Id,
                    versaoEsperada + i + 1,
                    e.GetType().AssemblyQualifiedName!,
                    JsonSerializer.Serialize(e)
                )).ToList();

        await _context.Eventos.AddRangeAsync(eventos);

        foreach (var e in usuario.Changes)
        {
            switch (e)
            {
                case UsuarioRegistrado ev:
                    _context.Usuarios.Add(new UsuarioProjecao
                    {
                        Id = ev.UsuarioId,
                        Nome = ev.Nome,
                        Email = ev.Email,
                        Role = "Usuario",
                        SenhaHash = ev.senha,
                        Ativo = true
                    });
                    break;

                case UsuarioPromovido ev:
                    var usuarioProj = await _context.Usuarios.FindAsync(ev.UsuarioId);
                    if (usuarioProj != null) usuarioProj.Role = "Admin";
                    break;

                case UsuarioAtualizado ev:
                    var usuarioUpdate = await _context.Usuarios.FindAsync(ev.UsuarioId);
                    if (usuarioUpdate != null)
                    {
                        if (!string.IsNullOrEmpty(ev.Nome))
                            usuarioUpdate.Nome = ev.Nome;

                        if (!string.IsNullOrEmpty(ev.Email))
                            usuarioUpdate.Email = ev.Email;

                        if (!string.IsNullOrEmpty(ev.Role))
                            usuarioUpdate.Role = ev.Role;

                        if (!string.IsNullOrEmpty(ev.NovaSenha))
                            usuarioUpdate.SenhaHash = ev.NovaSenha;
                    }
                    break;

                case UsuarioExcluido ev:
                    var usuarioExcluir = await _context.Usuarios.FindAsync(ev.UsuarioId);
                    if (usuarioExcluir != null)
                        usuarioExcluir.Ativo = false;
                    break;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<int> ObterVersaoAsync(Guid id)
    {
        var versao = await _context.Eventos
            .Where(e => e.AggregateId == id)
            .MaxAsync(e => (int?)e.Versao);
        return versao ?? 0;
    }
}
