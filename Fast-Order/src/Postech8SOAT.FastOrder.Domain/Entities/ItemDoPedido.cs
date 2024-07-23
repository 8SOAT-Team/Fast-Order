using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Entities;
public class ItemDoPedido : Entity
{
    protected ItemDoPedido() { }

    public Guid PedidoId { get; init; }

    public ItemDoPedido(Guid pedidoId, Guid produtoId, int quantidade)
    {
        ValidateDomain(pedidoId, produtoId, quantidade);
        Id = Guid.NewGuid();
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }

    public virtual Pedido Pedido { get; init; } = null!;
    public Guid ProdutoId { get; init; }
    public virtual Produto Produto { get; init; } = null!;
    public int Quantidade { get; private set; }

    private static void ValidateDomain(Guid pedidoId, Guid produtoId, int quantidade)
    {
        DomainExceptionValidation.When(pedidoId.Equals(Guid.Empty), $"Obrigatório informar um {nameof(pedidoId)} válido.");
        DomainExceptionValidation.When(produtoId.Equals(Guid.Empty), $"Obrigatório informar um {nameof(produtoId)} válido.");
        DomainExceptionValidation.When(quantidade <= 0, $"Obrigatório informar uma {nameof(quantidade)} maior que zero.");
    }

    public void Adicionar(int quantidade)
    {
        Quantidade += quantidade;
    }
    public void Remover(int quantidade)
    {
        Quantidade -= quantidade;
    }
}
