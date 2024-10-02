namespace Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

public record ItemDoPedidoDTO
{
    public Guid Id { get; init; }
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
}
