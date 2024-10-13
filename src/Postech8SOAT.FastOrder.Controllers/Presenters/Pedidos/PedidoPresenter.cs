using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Presenters.Pagamentos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Presenters.Clientes;

namespace Postech8SOAT.FastOrder.Controllers.Presenters.Pedidos;

internal static class PedidoPresenter
{
    public static PedidoDTO ToPedidoDTO(this Pedido pedido)
    {
        return new PedidoDTO
        {
            Id = pedido.Id,
            DataPedido = pedido.DataPedido,
            StatusPedido = pedido.StatusPedido,
            Cliente = pedido.Cliente is null ? null : ClientePresenter.AdaptCliente(pedido.Cliente!),
            ItensDoPedido = pedido.ItensDoPedido.Select(p => new ItemDoPedidoDTO()
            {
                Id = p.Id,
                ProdutoId = p.ProdutoId,
                Quantidade = p.Quantidade,
                Imagem = p.Produto?.Imagem!
            }).ToList(),
            ValorTotal = pedido.ValorTotal,
            Pagamento = pedido.Pagamento is null ? null : PagamentoPresenter.ToPagamentoDTO(pedido.Pagamento)
        };
    }

    public static List<PedidoDTO> ToListPedidoDTO(this List<Pedido> pedidos) => pedidos.Select(p => p.ToPedidoDTO()).ToList();
}
