using Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Pedidos.Dtos;
public class NovoPedidoDTOTest
{

    [Fact]
    public void DeveCriarNovoPedidoDTO_ComClienteIdValidoEItens()
    {
        // Arrange
        Guid clienteId = Guid.NewGuid();
        var item1 = new ItemDoPedidoDTO
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 5
        };
        var item2 = new ItemDoPedidoDTO
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 2
        };
        var itens = new List<ItemDoPedidoDTO> { item1, item2 };

        // Act
        var novoPedido = new NovoPedidoDTO
        {
            ClienteId = clienteId,
            ItensDoPedido = itens
        };

        // Assert
        Assert.Equal(clienteId, novoPedido.ClienteId);
        Assert.Equal(itens.Count, novoPedido.ItensDoPedido.Count);
        Assert.Equal(item1, novoPedido.ItensDoPedido[0]);
        Assert.Equal(item2, novoPedido.ItensDoPedido[1]);
    }

    [Fact]
    public void DeveCriarNovoPedidoDTO_ComClienteIdNulo()
    {
        // Arrange
        var item1 = new ItemDoPedidoDTO
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 5
        };
        var item2 = new ItemDoPedidoDTO
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 3
        };
        var itens = new List<ItemDoPedidoDTO> { item1, item2 };

        // Act
        var novoPedido = new NovoPedidoDTO
        {
            ClienteId = null,  
            ItensDoPedido = itens
        };

        // Assert
        Assert.Null(novoPedido.ClienteId);
        Assert.Equal(itens.Count, novoPedido.ItensDoPedido.Count);
    }

    [Fact]
    public void DeveCriarNovoPedidoDTO_ComItensVazios()
    {
        // Arrange
        var itensVazios = new List<ItemDoPedidoDTO>();

        // Act
        var novoPedido = new NovoPedidoDTO
        {
            ClienteId = Guid.NewGuid(),
            ItensDoPedido = itensVazios
        };

        // Assert
        Assert.Empty(novoPedido.ItensDoPedido); 
    }

    [Fact]
    public void DeveAdicionarItensNaListaDeItensDoPedido()
    {
        // Arrange
        var item1 = new ItemDoPedidoDTO
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 5
        };
        var item2 = new ItemDoPedidoDTO
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 3
        };
        var itens = new List<ItemDoPedidoDTO> { item1 };

        // Act
        itens.Add(item2);
        var novoPedido = new NovoPedidoDTO
        {
            ClienteId = Guid.NewGuid(),
            ItensDoPedido = itens
        };

        // Assert
        Assert.Equal(2, novoPedido.ItensDoPedido.Count); 
        Assert.Equal(item1, novoPedido.ItensDoPedido[0]);
        Assert.Equal(item2, novoPedido.ItensDoPedido[1]);
    }

}
