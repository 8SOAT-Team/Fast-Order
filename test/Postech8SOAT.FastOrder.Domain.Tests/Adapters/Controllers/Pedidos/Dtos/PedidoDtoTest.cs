using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Pedidos.Dtos;

public class PedidoDtoTest
{
    [Fact]
    public void PedidoDTO_ShouldInitializeCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dataPedido = DateTime.Now;
        var statusPedido = StatusPedido.Recebido;
        var cliente = new ClienteDTO { Id = Guid.NewGuid(), Nome = "John Doe", Email = "john.doe@example.com", Cpf = "12345678901" };
        var itensDoPedido = new List<ItemDoPedidoDTO>
        {
            new ItemDoPedidoDTO { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 2, Imagem = "http://example.com/image1.jpg" },
            new ItemDoPedidoDTO { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 3, Imagem = "http://example.com/image2.jpg" }
        };
        var valorTotal = 150.75m;
        var pagamento = new PagamentoResponseDTO(Guid.NewGuid(), MetodosDePagamento.Master, StatusDoPagamento.Autorizado, 150.75m, "external123", "http://payment.url");

        // Act
        var dto = new PedidoDTO
        {
            Id = id,
            DataPedido = dataPedido,
            StatusPedido = statusPedido,
            Cliente = cliente,
            ItensDoPedido = itensDoPedido,
            ValorTotal = valorTotal,
            Pagamento = pagamento
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(dataPedido, dto.DataPedido);
        Assert.Equal(statusPedido, dto.StatusPedido);
        Assert.Equal(cliente, dto.Cliente);
        Assert.Equal(itensDoPedido, dto.ItensDoPedido);
        Assert.Equal(valorTotal, dto.ValorTotal);
        Assert.Equal(pagamento, dto.Pagamento);
    }

    [Fact]
    public void PedidoDTO_ShouldHandleNullClienteAndPagamento()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dataPedido = DateTime.Now;
        var statusPedido = StatusPedido.Pronto;
        ClienteDTO? cliente = null;
        var itensDoPedido = new List<ItemDoPedidoDTO>
        {
            new ItemDoPedidoDTO { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 2, Imagem = "http://example.com/image1.jpg" },
            new ItemDoPedidoDTO { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 3, Imagem = "http://example.com/image2.jpg" }
        };
        var valorTotal = 150.75m;
        PagamentoResponseDTO? pagamento = null;

        // Act
        var dto = new PedidoDTO
        {
            Id = id,
            DataPedido = dataPedido,
            StatusPedido = statusPedido,
            Cliente = cliente,
            ItensDoPedido = itensDoPedido,
            ValorTotal = valorTotal,
            Pagamento = pagamento
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(dataPedido, dto.DataPedido);
        Assert.Equal(statusPedido, dto.StatusPedido);
        Assert.Null(dto.Cliente);
        Assert.Equal(itensDoPedido, dto.ItensDoPedido);
        Assert.Equal(valorTotal, dto.ValorTotal);
        Assert.Null(dto.Pagamento);
    }
}