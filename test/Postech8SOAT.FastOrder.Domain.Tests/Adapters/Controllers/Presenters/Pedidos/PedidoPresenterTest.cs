using Postech8SOAT.FastOrder.Controllers.Presenters.Pedidos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Tests.Stubs.Pedidos;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Presenters.Pedidos;

public class PedidoPresenterTest
{
    [Fact]
    public void ToPedidoDTO_ShouldReturnCorrectDto()
    {
        // Arrange
        var pedido = PedidoStubBuilder.Create();

        // Act
        var result = PedidoPresenter.ToPedidoDTO(pedido);

        // Assert
        Assert.Equal(pedido.Id, result.Id);
        Assert.Equal(pedido.DataPedido, result.DataPedido);
        Assert.Equal(pedido.StatusPedido, result.StatusPedido);
        Assert.Equal(pedido.Cliente?.Id, result.Cliente?.Id);
        Assert.Equal(pedido.ItensDoPedido.Count, result.ItensDoPedido.Count);
        Assert.Equal(pedido.ValorTotal, result.ValorTotal);
        Assert.Equal(pedido.Pagamento?.Id, result.Pagamento?.Id);
    }

    [Fact]
    public void ToListPedidoDTO_ShouldReturnCorrectDtoList()
    {
        // Arrange
        var pedidos = new List<Pedido>
        {
            PedidoStubBuilder.Create(),
            PedidoStubBuilder.Create()
        };

        // Act
        var result = PedidoPresenter.ToListPedidoDTO(pedidos);

        // Assert
        Assert.Equal(pedidos.Count, result.Count);
        for (int i = 0; i < pedidos.Count; i++)
        {
            Assert.Equal(pedidos[i].Id, result[i].Id);
            Assert.Equal(pedidos[i].DataPedido, result[i].DataPedido);
            Assert.Equal(pedidos[i].StatusPedido, result[i].StatusPedido);
            Assert.Equal(pedidos[i].Cliente?.Id, result[i].Cliente?.Id);
            Assert.Equal(pedidos[i].ItensDoPedido.Count, result[i].ItensDoPedido.Count);
            Assert.Equal(pedidos[i].ValorTotal, result[i].ValorTotal);
            Assert.Equal(pedidos[i].Pagamento?.Id, result[i].Pagamento?.Id);
        }
    }
}