using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

public record NovoStatusDePedidoDTO
{
    public Guid PedidoId { get; init; }
    public StatusPedido NovoStatus { get; init; }
}