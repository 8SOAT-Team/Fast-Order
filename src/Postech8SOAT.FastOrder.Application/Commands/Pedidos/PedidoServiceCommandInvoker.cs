using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.Application.Commands.Pedidos;


public class PedidoServiceCommandInvoker : IPedidoServiceCommandInvoker
{
    private static readonly Dictionary<StatusPedido, IPedidoServiceCommand> _comandos = new() {
        { IniciarPreparoPedidoCommand.Status, new IniciarPreparoPedidoCommand() },
        { FinalizarPreparoPedidoCommand.Status, new FinalizarPreparoPedidoCommand() },
        { EntregarPedidoCommand.Status, new EntregarPedidoCommand() },
        { CancelarPedidoCommand.Status, new CancelarPedidoCommand() },
    };

    public Task ExecutarComandoAsync(StatusPedido status, Guid pedidoId, IPedidoService pedidoService)
    {
        if (_comandos.TryGetValue(status, out var comando) is false)
        {
            throw new ArgumentException("Status de pedido desconhecido.");
        }

        return comando.ExecutarAsync(pedidoService, pedidoId);
    }
}
