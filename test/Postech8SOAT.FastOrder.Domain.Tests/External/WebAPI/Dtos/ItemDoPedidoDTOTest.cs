using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.WebAPI.Dtos;

public class ItemDoPedidoDTOTest
{
    [Fact]
    public void ItemDoPedidoDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var produtoId = Guid.NewGuid();
        var quantidade = 5;

        // Act
        var dto = new ItemDoPedidoDTO
        {
            ProdutoId = produtoId,
            Quantidade = quantidade
        };
        dto.SetId(id);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(produtoId, dto.ProdutoId);
        Assert.Equal(quantidade, dto.Quantidade);
    }
}