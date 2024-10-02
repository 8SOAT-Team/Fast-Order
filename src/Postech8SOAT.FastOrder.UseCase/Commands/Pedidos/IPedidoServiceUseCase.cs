

using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.UseCases.Commands.Pedidos;

public interface IPedidoServiceUseCase

{
    Task ExecutarAsync(IPedidoUseCase service, Guid pedidoId);
}
