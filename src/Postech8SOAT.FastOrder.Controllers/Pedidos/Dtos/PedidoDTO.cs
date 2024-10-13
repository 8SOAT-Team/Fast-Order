﻿using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

public record PedidoDTO
{
    public Guid Id { get; init; }
    public DateTime DataPedido { get; init; }
    public StatusPedido StatusPedido { get; init; }
    public virtual ClienteDTO? Cliente { get; init; }
    public virtual IReadOnlyCollection<ItemDoPedidoDTO> ItensDoPedido { get; init; } = Array.Empty<ItemDoPedidoDTO>();
    public decimal ValorTotal { get; init; }
    public virtual PagamentoResponseDTO? Pagamento { get; init; }
}
