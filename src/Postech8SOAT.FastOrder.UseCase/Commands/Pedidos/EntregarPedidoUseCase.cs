using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;


namespace Postech8SOAT.FastOrder.UseCases.Commands.Pedidos;

internal class EntregarPedidoUseCase : IPedidoServiceUseCase
{
    public const StatusPedido Status = StatusPedido.Finalizado;

    public Task ExecutarAsync(IPedidoUseCase pedido, Guid pedidoId) => pedido.Entregar(pedidoId);
}
