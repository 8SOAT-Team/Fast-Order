using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Common;
using Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.UseCases.Pedidos;

public class CriarNovoPedidoUseCase(ILogger logger,
    IPedidoGateway pedidoGateway,
    IProdutoGateway produtoGateway) : UseCase<NovoPedidoDTO, Pedido>(logger)
{
    private readonly IPedidoGateway _pedidoGateway = pedidoGateway;
    private readonly IProdutoGateway _produtoGateway = produtoGateway;

    protected override async Task<Pedido?> Execute(NovoPedidoDTO command)
    {
        var productsIds = command.ItensDoPedido.Select(i => i.ProdutoId);
        var productsEntityList = (await _produtoGateway.ListarProdutosByIdAsync(productsIds.ToArray())).ToList();
        var missingProducts = productsIds.Except(productsEntityList.Select(p => p.Id)).ToArray();

        if (missingProducts.Length > 0)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, $"Produto não encontrado: {string.Join(", ", missingProducts)}"));
            return null;
        }

        var pedidoId = Guid.NewGuid();
        var orderItems = command.ItensDoPedido.Select(i => MapItemDoPedido(i, pedidoId, productsEntityList.First(p => p.Id == i.ProdutoId))).ToList();

        var pedido = new Pedido(pedidoId, command.ClienteId, orderItems);
        var pedidoEntity = await _pedidoGateway.CreateAsync(pedido);

        return pedidoEntity;
    }

    private static ItemDoPedido MapItemDoPedido(ItemDoPedidoDTO itemDoPedidoDTO, Guid pedidoId, Produto produto)
        => new(pedidoId, produto, itemDoPedidoDTO.Quantidade);
}
