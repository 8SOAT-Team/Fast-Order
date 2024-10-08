using CleanArch.UseCase.Logging;
using CleanArch.UseCase.Options;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Common;

namespace Postech8SOAT.FastOrder.UseCases.Pedidos;

public class ObterListaPedidosPendentesUseCase(ILogger logger, IPedidoGateway pedidoGateway) : UseCase<Any<object>, List<Pedido>>(logger)
{
    private readonly IPedidoGateway _pedidoGateway = pedidoGateway;

    protected override async Task<List<Pedido>?> Execute(Any<object> command)
    {
        return await _pedidoGateway.GetAllPedidosPending();
    }
}