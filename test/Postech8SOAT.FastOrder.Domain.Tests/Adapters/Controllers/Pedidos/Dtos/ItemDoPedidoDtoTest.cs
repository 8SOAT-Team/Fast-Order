using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Pedidos.Dtos;

public class ItemDoPedidoDtoTest
{
    [Fact]
    public void ItemDoPedidoDTO_ShouldInitializeCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var produtoId = Guid.NewGuid();
        var quantidade = 5;
        var imagem = "http://example.com/image.jpg";

        // Act
        var dto = new ItemDoPedidoDTO
        {
            Id = id,
            ProdutoId = produtoId,
            Quantidade = quantidade,
            Imagem = imagem
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(produtoId, dto.ProdutoId);
        Assert.Equal(quantidade, dto.Quantidade);
        Assert.Equal(imagem, dto.Imagem);
    }

    [Fact]
    public void ItemDoPedidoDTO_ShouldHandleNullImagem()
    {
        // Arrange
        var id = Guid.NewGuid();
        var produtoId = Guid.NewGuid();
        var quantidade = 5;
        string? imagem = null;

        // Act
        var dto = new ItemDoPedidoDTO
        {
            Id = id,
            ProdutoId = produtoId,
            Quantidade = quantidade,
            Imagem = imagem
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(produtoId, dto.ProdutoId);
        Assert.Equal(quantidade, dto.Quantidade);
        Assert.Null(dto.Imagem);
    }
}