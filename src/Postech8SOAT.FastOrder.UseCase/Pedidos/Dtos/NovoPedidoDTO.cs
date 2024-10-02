namespace Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

public record NovoPedidoDTO
{
    public Guid? ClienteId { get; init; }
    public List<ItemDoPedidoDTO> ItensDoPedido { get; init; } = null!;
}
