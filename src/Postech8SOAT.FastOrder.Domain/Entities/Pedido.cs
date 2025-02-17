using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Postech8SOAT.FastOrder.Domain.Entities;

public sealed class Pedido : Entity, IAggregateRoot
{
    private const StatusPedido StatusInicial = StatusPedido.Recebido;
    private const StatusPedido StatusFinal = StatusPedido.Finalizado;

    private static readonly ImmutableHashSet<StatusPedido> StatusPedidoPermiteAlteracao =
        [StatusPedido.Recebido, StatusPedido.EmPreparacao];

    public DateTime DataPedido { get; private set; }
    public StatusPedido StatusPedido { get; private set; }
    public Guid? ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public ICollection<ItemDoPedido> ItensDoPedido { get; set; }
    public decimal ValorTotal { get; private set; }
    public Pagamento? Pagamento { get; private set; }

    public Pedido()
    {
    }

    public Pedido(Guid? clienteId, List<ItemDoPedido> itensDoPedido) : this(Guid.NewGuid(), clienteId, itensDoPedido)
    {
    }

    [JsonConstructor]
    public Pedido(Guid id, Guid? clienteId, List<ItemDoPedido> itensDoPedido)
    {
        ValidationDomain(id, clienteId, itensDoPedido);

        Id = id;
        ClienteId = clienteId;
        ItensDoPedido = itensDoPedido;
        DataPedido = DateTime.Now;
        StatusPedido = StatusInicial;
        CalcularValorTotal();
    }

    public void CalcularValorTotal()
    {
        ValorTotal = ItensDoPedido?.Sum(item => item.Produto.Preco * item.Quantidade) ?? 0;
    }

    private static void ValidationDomain(Guid id, Guid? clienteId, List<ItemDoPedido> itens)
    {
        DomainExceptionValidation.When(id == Guid.Empty, "Id inválido");
        DomainExceptionValidation.When(clienteId == Guid.Empty, "Informar um id de cliente válido é obrigatório");
        DomainExceptionValidation.When(itens.Count <= 0, "O pedido deve conter pelo menos um item");
    }

    public Pedido IniciarPreparo(Pagamento? pagamento = null)
    {
        pagamento ??= Pagamento;
        Pagamento = pagamento;

        DomainExceptionValidation.When(StatusPedido != StatusPedido.Recebido,
            $"Status do pedido não permite iniciar preparo. O status deve ser {StatusPedido.Recebido} para iniciar o preparo.");

        DomainExceptionValidation.When(Pagamento?.EstaAutorizado() is not true,
            $"O pedido não tem um pagamento confirmado. Não é possível iniciar o preparo.");

        StatusPedido = StatusPedido.EmPreparacao;
        return this;
    }

    public Pedido FinalizarPreparo()
    {
        DomainExceptionValidation.When(StatusPedido != StatusPedido.EmPreparacao,
            $"Status do pedido não permite finalizar o preparo. O status deve ser {StatusPedido.EmPreparacao} para finalizar o preparo.");

        StatusPedido = StatusPedido.Pronto;
        return this;
    }

    public Pedido Entregar()
    {
        DomainExceptionValidation.When(StatusPedido != StatusPedido.Pronto,
            $"O pedido deve estar {StatusPedido.Pronto} para realizar a entrega.");

        StatusPedido = StatusFinal;
        return this;
    }

    public Pedido Cancelar()
    {
        DomainExceptionValidation.When(StatusPedido == StatusFinal,
            $"O pedido não pode ser cancelado após ser {StatusFinal}.");

        StatusPedido = StatusPedido.Cancelado;
        return this;
    }

    public void IniciarPagamento(MetodoDePagamento metodoDePagamento)
    {
        DomainExceptionValidation.When(StatusPedido != StatusInicial,
            $"Status do pedido não permite pagamento. O status deve ser {StatusPedido.Recebido} para realizar o pagamento.");

        Pagamento = new Pagamento(this, metodoDePagamento, this.ValorTotal, null);
    }
}