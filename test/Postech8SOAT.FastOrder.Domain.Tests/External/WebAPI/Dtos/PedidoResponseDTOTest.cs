using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Tests.Stubs.Pedidos;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.WebAPI.Dtos;

public class PedidoResponseDTOTest
{
    [Fact]
    public void PedidoResponseDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dataPedido = DateTime.Now;
        var statusPedido = StatusPedido.Recebido;
        var clienteId = Guid.NewGuid();
        var cliente = ClienteStubBuilder.Create();
        var itensDoPedido = new List<ItemDoPedidoDTO>
        {
            new ItemDoPedidoDTO { ProdutoId = Guid.NewGuid(), Quantidade = 2 },
            new ItemDoPedidoDTO { ProdutoId = Guid.NewGuid(), Quantidade = 3 }
        };
        var valorTotal = 150.75m;

        // Act
        var dto = new PedidoResponseDTO
        {
            Id = id,
            DataPedido = dataPedido,
            StatusPedido = statusPedido,
            ClienteId = clienteId,
            Cliente = cliente,
            ItensDoPedido = itensDoPedido,
            ValorTotal = valorTotal
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(dataPedido, dto.DataPedido);
        Assert.Equal(statusPedido, dto.StatusPedido);
        Assert.Equal(clienteId, dto.ClienteId);
        Assert.Equal(cliente, dto.Cliente);
        Assert.Equal(itensDoPedido, dto.ItensDoPedido);
        Assert.Equal(valorTotal, dto.ValorTotal);
    }
}