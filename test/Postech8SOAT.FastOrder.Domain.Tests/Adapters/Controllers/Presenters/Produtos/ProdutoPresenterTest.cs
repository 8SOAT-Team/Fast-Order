using Postech8SOAT.FastOrder.Controllers.Presenters.Produtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Tests.Stubs.Pedidos;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Presenters.Produtos;

public class ProdutoPresenterTest
{
    [Fact]
    public void AdaptProdutoCriado_ShouldReturnCorrectDto()
    {
        // Arrange
        var produto = ProdutoStubBuilder.Create();

        // Act
        var result = ProdutoPresenter.AdaptProdutoCriado(produto);

        // Assert
        Assert.Equal(produto.Id, result.Id);
        Assert.Equal(produto.Nome, result.Nome);
        Assert.Equal(produto.Descricao, result.Descricao);
        Assert.Equal(produto.Preco, result.Preco);
        Assert.Equal(produto.Imagem, result.Imagem);
        Assert.Equal(produto.Categoria.Id, result.Categoria.Id);
        Assert.Equal(produto.Categoria.Nome, result.Categoria.Nome);
    }

    [Fact]
    public void AdaptProduto_ShouldReturnCorrectDto()
    {
        // Arrange
        var produto = ProdutoStubBuilder.Create();

        // Act
        var result = ProdutoPresenter.AdaptProduto(produto);

        // Assert
        Assert.Equal(produto.Id, result.Id);
        Assert.Equal(produto.Nome, result.Nome);
        Assert.Equal(produto.Descricao, result.Descricao);
        Assert.Equal(produto.Preco, result.Preco);
        Assert.Equal(produto.Imagem, result.Imagem);
        Assert.Equal(produto.Categoria.Id, result.Categoria.Id);
        Assert.Equal(produto.Categoria.Nome, result.Categoria.Nome);
    }

    [Fact]
    public void AdaptProdutoList_ShouldReturnCorrectDtoList()
    {
        // Arrange
        var produtos = new List<Produto>
        {
            ProdutoStubBuilder.Create(),
            ProdutoStubBuilder.Create()
        };

        // Act
        var result = ProdutoPresenter.AdaptProduto(produtos).ToList();

        // Assert
        Assert.Equal(produtos.Count, result.Count);
        for (int i = 0; i < produtos.Count; i++)
        {
            Assert.Equal(produtos[i].Id, result[i].Id);
            Assert.Equal(produtos[i].Nome, result[i].Nome);
            Assert.Equal(produtos[i].Descricao, result[i].Descricao);
            Assert.Equal(produtos[i].Preco, result[i].Preco);
            Assert.Equal(produtos[i].Imagem, result[i].Imagem);
            Assert.Equal(produtos[i].Categoria.Id, result[i].Categoria.Id);
            Assert.Equal(produtos[i].Categoria.Nome, result[i].Categoria.Nome);
        }
    }
}