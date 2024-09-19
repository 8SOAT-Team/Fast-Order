using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;


namespace Postech8SOAT.FastOrder.UseCases.Commands.Pedidos;


internal class IniciarPreparoPedidoUseCase : IPedidoServiceUseCase
{
    public const StatusPedido Status = StatusPedido.EmPreparacao;
    public Task ExecutarAsync(IPedidoUseCase pedido, Guid pedidoId) => pedido.IniciarPreparo(pedidoId);
}
