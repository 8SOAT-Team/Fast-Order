using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Common;
using Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.UseCases.Pedidos;

public class AtualizarStatusDePreparoPedidoUseCase(ILogger logger,
    IPedidoGateway pedidoGateway) : UseCase<NovoStatusDePedidoDTO, Pedido>(logger)
{
    private static readonly Dictionary<StatusPedido, Func<Pedido, Pedido>> _actionUpdateStatus = new()
    {
        { StatusPedido.EmPreparacao, p => p.IniciarPreparo() },
        { StatusPedido.Pronto, p => p.FinalizarPreparo() },
        { StatusPedido.Finalizado, p => p.Entregar() },
        { StatusPedido.Cancelado, p => p.Cancelar() },
    };

    private readonly IPedidoGateway _pedidoGateway = pedidoGateway;

    protected async override Task<Pedido?> Execute(NovoStatusDePedidoDTO request)
    {
        var pedido = await _pedidoGateway.GetByIdAsync(request.PedidoId);

        if (pedido is null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Pedido não encontrado"));
            return null;
        }

        if (!_actionUpdateStatus.TryGetValue(request.NovoStatus, out var action))
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Status de pedido inválido"));
            return null;
        }

        _ = action(pedido);
        return await _pedidoGateway.UpdateAsync(pedido);
    }
}
