using Postech8SOAT.FastOrder.Controllers.Presenters.Produtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Tests.Stubs.Pedidos;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Presenters.Produtos;

public class CategoriaAdapterTest
{
    [Fact]
    public void AdaptCategoria_ShouldReturnCorrectDto()
    {
        // Arrange
        var categoria = CategoriaStubBuilder.Create();

        // Act
        var result = CategoriaAdapter.AdaptCategoria(categoria);

        // Assert
        Assert.Equal(categoria.Id, result.Id);
        Assert.Equal(categoria.Nome, result.Nome);
    }

    [Fact]
    public void AdaptCategoriaList_ShouldReturnCorrectDtoList()
    {
        // Arrange
        var categorias = new List<Categoria>
        {
            CategoriaStubBuilder.Create(),
            CategoriaStubBuilder.Create()
        };

        // Act
        var result = CategoriaAdapter.AdaptCategoria(categorias).ToList();

        // Assert
        Assert.Equal(categorias.Count, result.Count);
        for (int i = 0; i < categorias.Count; i++)
        {
            Assert.Equal(categorias[i].Id, result[i].Id);
            Assert.Equal(categorias[i].Nome, result[i].Nome);
        }
    }
}