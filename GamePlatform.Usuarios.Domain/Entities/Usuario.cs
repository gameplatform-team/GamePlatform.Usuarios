using GamePlatform.Usuarios.Domain.Events;

namespace GamePlatform.Usuarios.Domain.Entities;

public class Usuario : BaseEntity
{
    private readonly List<object> _changes = new();
    public IReadOnlyCollection<object> Changes => _changes.AsReadOnly();

    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    public string Role { get; private set; } // "Usuario" ou "Admin"
    public bool Ativo { get; private set; }

    private Usuario() { }

    private Usuario(Guid id, string nome, string email, string senhaHash)
    {
        Id = id;
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Role = "Usuario";
        Ativo = true;
    }

    #region Registrar
    public static Usuario Registrar(Guid id, string nome, string email, string senhaHash)
    {
        var usuario = new Usuario(id, nome, email, senhaHash);
        usuario.AdicionarEvento(new UsuarioRegistrado(id, nome, email, senhaHash, true));
        return usuario;
    }
    #endregion

    #region Atualizar
    public void Atualizar(bool ehAdmin, string? nome = null, string? email = null, string? novaSenha = null, string? role = null)
    {
        string? nomeAlterado = (!string.IsNullOrEmpty(nome) && nome != Nome) ? nome : null;
        string? emailAlterado = (!string.IsNullOrEmpty(email) && email != Email) ? email : null;
        string? senhaHash = !string.IsNullOrEmpty(novaSenha) ? BCrypt.Net.BCrypt.HashPassword(novaSenha) : null;
        string? roleAlterada = (ehAdmin && !string.IsNullOrEmpty(role) && role != Role) ? role : null;

        if (nomeAlterado == null && emailAlterado == null && senhaHash == null && roleAlterada == null)
            return;

        var evento = new UsuarioAtualizado(Id, nomeAlterado, emailAlterado, senhaHash, roleAlterada);
        Aplicar(evento);
        _changes.Add(evento);
    }

    private void Aplicar(UsuarioAtualizado e)
    {
        if (!string.IsNullOrEmpty(e.Nome))
            Nome = e.Nome;

        if (!string.IsNullOrEmpty(e.Email))
            Email = e.Email;

        if (!string.IsNullOrEmpty(e.NovaSenha))
            SenhaHash = e.NovaSenha;

        if (!string.IsNullOrEmpty(e.Role))
            Role = e.Role;
    }
    #endregion

    #region Excluir
    public void Excluir()
    {
        if (!Ativo) return;
        Ativo = false;
        AdicionarEvento(new UsuarioExcluido(Id));
    }

    private void Aplicar(UsuarioExcluido e)
    {
        Ativo = false;
    }
    #endregion

    #region Promover
    public void Promover()
    {
        if (Role == "Admin") return;

        Role = "Admin";
        AdicionarEvento(new UsuarioPromovido(Id));
    }
    #endregion

    public void Aplicar(object @event)
    {
        switch (@event)
        {
            case UsuarioRegistrado e:
                Id = e.UsuarioId;
                Nome = e.Nome;
                Email = e.Email;
                Role = "Usuario";
                Ativo = true;
                break;

            case UsuarioPromovido:
                Role = "Admin";
                break;

            case UsuarioAtualizado e:
                Aplicar(e);
                break;

            case UsuarioExcluido e:
                Aplicar(e);
                break;
        }
    }

    public static Usuario Reconstituir(IEnumerable<object> eventos)
    {
        var usuario = new Usuario();
        foreach (var e in eventos) usuario.Aplicar(e);
        return usuario;
    }

    private void AdicionarEvento(object evento) => _changes.Add(evento);
}
