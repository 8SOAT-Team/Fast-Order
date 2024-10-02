using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;


namespace Postech8SOAT.FastOrder.UseCases.Commands.Pedidos;


public class PedidoServiceUseCaseInvoker : IPedidoServiceUseCaseInvoker
{
    private static readonly Dictionary<StatusPedido, IPedidoServiceUseCase> _comandos = new() {
        { IniciarPreparoPedidoUseCase.Status, new IniciarPreparoPedidoUseCase() },
        { FinalizarPreparoPedidoUseCase.Status, new FinalizarPreparoPedidoUseCase() },
        { EntregarPedidoUseCase.Status, new EntregarPedidoUseCase() },
        { CancelarPedidoUseCase.Status, new CancelarPedidoUseCase() },
    };

    public Task ExecutarComandoAsync(StatusPedido status, Guid pedidoId, IPedidoUseCase pedidoService)
    {
        if (_comandos.TryGetValue(status, out var comando) is false)
        {
            throw new ArgumentException("Status de pedido desconhecido.");
        }

        return comando.ExecutarAsync(pedidoService, pedidoId);
    }
}
