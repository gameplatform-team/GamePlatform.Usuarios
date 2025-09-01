namespace GamePlatform.Usuarios.Domain.Entities;

public class Evento
{
    public Guid Id { get; private set; }
    public Guid AggregateId { get; private set; }
    public int Versao { get; private set; }
    public string TipoEvento { get; private set; } = default!;
    public string Dados { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }

    private Evento() { }

    public Evento(Guid aggregateId, int versao, string tipoEvento, string dados)
    {
        Id = Guid.NewGuid();
        AggregateId = aggregateId;
        Versao = versao;
        TipoEvento = tipoEvento;
        Dados = dados;
        CreatedAt = DateTime.UtcNow;
    }
}
