namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public record NovoPedidoDTO
{
    public Guid ClienteId { get; init; }
    public List<ItemDoPedidoDTO> ItensDoPedido { get; init; } = null!;
}
