using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Presenters.Clientes;

namespace Postech8SOAT.FastOrder.Controllers.Presenters.Pedidos;

internal static class PedidoPresenter
{
    public static PedidoCriadoDTO ToPedidoCriadoDTO(this Pedido pedido)
    {
        return new PedidoCriadoDTO
        {
            Id = pedido.Id,
            DataPedido = pedido.DataPedido,
            StatusPedido = pedido.StatusPedido,
            Cliente = pedido.Cliente is null ? null : ClientePresenter.AdaptCliente(pedido.Cliente!),
            ItensDoPedido = pedido.ItensDoPedido.Select(p => (ItemDoPedidoDTO)p).ToList(),
            ValorTotal = pedido.ValorTotal,
            Pagamento = pedido.Pagamento
        };
    }
}
