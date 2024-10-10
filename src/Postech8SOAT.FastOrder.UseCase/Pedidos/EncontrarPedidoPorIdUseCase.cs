using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Common;

namespace Postech8SOAT.FastOrder.UseCases.Pedidos;

public class EncontrarPedidoPorIdUseCase(ILogger logger, IPedidoGateway pedidoGateway) : UseCase<Guid, Pedido>(logger)
{
    private readonly IPedidoGateway _pedidoGateway = pedidoGateway;

    protected override async Task<Pedido?> Execute(Guid pedidoId)
    {
        return await _pedidoGateway.GetByIdAsync(pedidoId);
    }
}