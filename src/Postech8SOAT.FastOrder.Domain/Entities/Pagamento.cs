using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using System.Collections.Immutable;

namespace Postech8SOAT.FastOrder.Domain.Entities;

public class Pagamento : Entity, IAggregateRoot
{
    private static readonly ImmutableDictionary<MetodoDePagamento, MetodoDePagamento[]> _metodosDePagamentosObrigatorios = new Dictionary<MetodoDePagamento, MetodoDePagamento[]>()
    {
        { MetodoDePagamento.Cartao, new MetodoDePagamento[] { MetodoDePagamento.Master, MetodoDePagamento.Visa } },
        { MetodoDePagamento.Pix, Array.Empty<MetodoDePagamento>() }
    }
    .ToImmutableDictionary();

    public const int Parcelas = 1;

    protected Pagamento() { }

    public Pagamento(Guid id, Guid pedidoId, Pedido pedido, MetodoDePagamento metodoDePagamento, decimal valorTotal, string? pagamentoExternoId)
    {
        ValidationDomain(id, pedido, metodoDePagamento, valorTotal);

        Id = id;
        PedidoId = pedidoId;
        Pedido = pedido;
        MetodoDePagamento = metodoDePagamento;
        ValorTotal = valorTotal;
        PagamentoExternoId = pagamentoExternoId;
        Status = StatusPagamento.Pendente;
    }

    public Pagamento(Pedido pedido, MetodoDePagamento metodoDePagamento, decimal valorTotal, string? pagamentoExternoId)
    : this(Guid.NewGuid(), pedido.Id, pedido, metodoDePagamento, valorTotal, pagamentoExternoId) { }

    public Guid PedidoId { get; init; }
    public string? PagamentoExternoId { get; private set; }
    public virtual Pedido Pedido { get; init; } = null!;
    public StatusPagamento Status { get; private set; }
    public MetodoDePagamento MetodoDePagamento { get; init; }
    public decimal ValorTotal { get; init; }

    public bool EstaAutorizado() => Status == StatusPagamento.Autorizado;

    public void FinalizarPagamento(bool autorizado)
    {
        DomainExceptionValidation.When(Status != StatusPagamento.Pendente, $"Pagamento só pode ser confirmado quando o status atual é {StatusPagamento.Pendente}");
        Status = autorizado ? StatusPagamento.Autorizado : StatusPagamento.Rejeitado;
    }

    public void CancelarPagamento()
    {
        DomainExceptionValidation.When(Status != StatusPagamento.Pendente, $"Pagamento só pode ser cancelado quando o status atual é {StatusPagamento.Pendente}");
        Status = StatusPagamento.Cancelado;
    }

    public void AssociarPagamentoExterno(string pagamentoExternoId)
    {
        PagamentoExternoId = pagamentoExternoId;
    }

    private static void ValidationDomain(Guid id, Pedido pedido, MetodoDePagamento metodoDePagamento, decimal valorTotal)
    {
        DomainExceptionValidation.When(id == Guid.Empty, "Id inválido");
        DomainExceptionValidation.When(pedido is null || pedido.Id == Guid.Empty, "Pedido inválido");
        DomainExceptionValidation.When(Enum.IsDefined(metodoDePagamento) is false, "Método de pagamento inválido");
        DomainExceptionValidation.When(valorTotal < 0, "Valor total inválido");
        DomainExceptionValidation.When(ValidationMetodoDePagamentoCartao(metodoDePagamento) is false, "Método de pagamento inválido");
    }

    private static bool ValidationMetodoDePagamentoCartao(MetodoDePagamento metodoDePagamento)
    {
        foreach (var metodo in _metodosDePagamentosObrigatorios)
        {
            if (metodoDePagamento.HasFlag(metodo.Key))
            {
                var tipoDeMetodoNecessario = metodo.Value;

                if (tipoDeMetodoNecessario == Array.Empty<MetodoDePagamento>())
                {
                    continue;
                }

                var temAlgumTipo = false;

                foreach (var tipo in tipoDeMetodoNecessario)
                {
                    temAlgumTipo = metodoDePagamento.HasFlag(tipo);

                    if (temAlgumTipo) break;
                }

                if (temAlgumTipo is false)
                {
                    return temAlgumTipo;
                }
            }
        }

        return true;
    }
}
