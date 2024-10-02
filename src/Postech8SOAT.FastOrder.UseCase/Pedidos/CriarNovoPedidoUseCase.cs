using CleanArch.UseCase;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.UseCases.Pedidos;

public class CriarNovoPedidoUseCase : UseCaseBase<NovoPedidoDTO, Pedido>
{
    private readonly IPedidoGateway _pedidoGateway;
    public CriarNovoPedidoUseCase(ILogger logger, IPedidoGateway pedidoGateway) : base(logger)
    {
        _pedidoGateway = pedidoGateway;
    }

    protected override async Task<Pedido?> Execute(NovoPedidoDTO command)
    {
        var pedidoId = Guid.NewGuid();
        var pedido = new Pedido(pedidoId, command.ClienteId,
            command.ItensDoPedido.Select(i => new ItemDoPedido(pedidoId, i.ProdutoId, i.Quantidade)).ToList());

        //TODO: buscar produtos e calcular valor total

        var pedidoEntity = await _pedidoGateway.CreatePedidoAsync(pedido);

        return pedidoEntity;
    }
}
