using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public record PedidoResponseDTO
{
    public virtual Guid Id { get; init; }
    public DateTime DataPedido { get; init; }
    public StatusPedido StatusPedido { get; init; }
    public virtual Guid ClienteId { get; init; }
    public virtual Cliente? Cliente { get; init; }
    public virtual ICollection<ItemDoPedidoDTO> ItensDoPedido { get; init; } = null!;
    public decimal ValorTotal { get; init; }
}
