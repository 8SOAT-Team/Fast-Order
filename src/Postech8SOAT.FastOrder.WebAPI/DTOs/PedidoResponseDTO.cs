using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public record PedidoResponseDTO
{
    public Guid? Id { get; init; }
    public DateTime DataPedido { get; init; }
    public StatusPedido StatusPedido { get; init; }
    public Guid ClienteId { get; init; }
    public Cliente? Cliente { get; init; }
    public ICollection<ItemDoPedidoDTO> ItensDoPedido { get; init; } = null!;
    public decimal ValorTotal { get; init; }
}
