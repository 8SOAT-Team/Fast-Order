using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Pedidos.Dtos;

public class NovoPedidoDtoTest
{
    [Fact]
    public void NovoPedidoDTO_ShouldInitializeCorrectly()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var itensDoPedido = new List<NovoItemDePedido>
        {
            new NovoItemDePedido { ProdutoId = Guid.NewGuid(), Quantidade = 2 },
            new NovoItemDePedido { ProdutoId = Guid.NewGuid(), Quantidade = 3 }
        };

        // Act
        var dto = new NovoPedidoDTO
        {
            ClienteId = clienteId,
            ItensDoPedido = itensDoPedido
        };

        // Assert
        Assert.Equal(clienteId, dto.ClienteId);
        Assert.Equal(itensDoPedido, dto.ItensDoPedido);
    }

    [Fact]
    public void NovoPedidoDTO_ShouldHandleNullClienteId()
    {
        // Arrange
        Guid? clienteId = null;
        var itensDoPedido = new List<NovoItemDePedido>
        {
            new NovoItemDePedido { ProdutoId = Guid.NewGuid(), Quantidade = 2 },
            new NovoItemDePedido { ProdutoId = Guid.NewGuid(), Quantidade = 3 }
        };

        // Act
        var dto = new NovoPedidoDTO
        {
            ClienteId = clienteId,
            ItensDoPedido = itensDoPedido
        };

        // Assert
        Assert.Null(dto.ClienteId);
        Assert.Equal(itensDoPedido, dto.ItensDoPedido);
    }

    [Fact]
    public void NovoPedidoDTO_ShouldHandleEmptyItensDoPedido()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var itensDoPedido = new List<NovoItemDePedido>();

        // Act
        var dto = new NovoPedidoDTO
        {
            ClienteId = clienteId,
            ItensDoPedido = itensDoPedido
        };

        // Assert
        Assert.Equal(clienteId, dto.ClienteId);
        Assert.Empty(dto.ItensDoPedido);
    }
}