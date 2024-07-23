using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.WebAPI.DTOs;

public record AtualizarStatusDoPedidoDTO
{
    public StatusPedido NovoStatus { get; init; }
}