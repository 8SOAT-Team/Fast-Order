using Postech8SOAT.FastOrder.UseCases.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.Tests.Domain.UseCases.Pedidos.Dtos;
public class ItemDoPedidoDTOTest
{
    [Fact]
    public void DeveCriarItemDoPedidoDTO_ComValoresCorretos()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid produtoId = Guid.NewGuid();
        int quantidade = 10;

        // Act
        var item = new ItemDoPedidoDTO
        {
            Id = id,
            ProdutoId = produtoId,
            Quantidade = quantidade
        };

        // Assert
        Assert.Equal(id, item.Id);
        Assert.Equal(produtoId, item.ProdutoId);
        Assert.Equal(quantidade, item.Quantidade);
    }

    [Fact]
    public void DeveCriarItemDoPedidoDTO_ComValoresDiferentes()
    {
        // Arrange
        Guid id1 = Guid.NewGuid();
        Guid produtoId1 = Guid.NewGuid();
        int quantidade1 = 5;

        Guid id2 = Guid.NewGuid();
        Guid produtoId2 = Guid.NewGuid();
        int quantidade2 = 3;

        // Act
        var item1 = new ItemDoPedidoDTO
        {
            Id = id1,
            ProdutoId = produtoId1,
            Quantidade = quantidade1
        };

        var item2 = new ItemDoPedidoDTO
        {
            Id = id2,
            ProdutoId = produtoId2,
            Quantidade = quantidade2
        };

        // Assert
        Assert.NotEqual(item1, item2);
    }

    [Fact]
    public void DeveSerIgualQuandoDoMesmoValor()
    {
        // Arrange
        var id = Guid.NewGuid();
        var produtoId = Guid.NewGuid();
        var quantidade = 10;

        var item1 = new ItemDoPedidoDTO
        {
            Id = id,
            ProdutoId = produtoId,
            Quantidade = quantidade
        };

        var item2 = new ItemDoPedidoDTO
        {
            Id = id,
            ProdutoId = produtoId,
            Quantidade = quantidade
        };

        // Act and Assert
        Assert.Equal(item1, item2);
    }
}

