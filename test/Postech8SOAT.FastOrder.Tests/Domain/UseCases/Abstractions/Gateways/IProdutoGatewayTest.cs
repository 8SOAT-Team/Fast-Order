using Moq;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Tests.Domain.UseCases.Abstractions.Gateways;
public class IProdutoGatewayTest
{
    [Fact]
    public async Task CreateProdutoAsync_DeveCriarProduto()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produto = new Produto(Guid.NewGuid(), "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem.jpg", Guid.NewGuid());

        mockGateway.Setup(gateway => gateway.CreateProdutoAsync(produto)).ReturnsAsync(produto);

        // Act
        var resultado = await mockGateway.Object.CreateProdutoAsync(produto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produto.Id, resultado.Id);
        Assert.Equal(produto.Nome, resultado.Nome);
        Assert.Equal(produto.Preco, resultado.Preco);
    }

    [Fact]
    public async Task GetProdutoByIdAsync_DeveRetornarProdutoQuandoIdValido()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produtoId = Guid.NewGuid();
        var produto = new Produto(produtoId, "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem.jpg", Guid.NewGuid());

        mockGateway.Setup(gateway => gateway.GetProdutoByIdAsync(produtoId)).ReturnsAsync(produto);

        // Act
        var resultado = await mockGateway.Object.GetProdutoByIdAsync(produtoId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produtoId, resultado.Id);
    }

    [Fact]
    public async Task GetProdutoCompletoByIdAsync_DeveRetornarProdutoCompletoQuandoIdValido()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produtoId = Guid.NewGuid();
        var produto = new Produto(produtoId, "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem.jpg", Guid.NewGuid());

        mockGateway.Setup(gateway => gateway.GetProdutoCompletoByIdAsync(produtoId)).ReturnsAsync(produto);

        // Act
        var resultado = await mockGateway.Object.GetProdutoCompletoByIdAsync(produtoId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produtoId, resultado.Id);
    }


    [Fact]
    public async Task GetProdutosByCategoriaAsync_DeveRetornarProdutosDaCategoria()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var categoriaId = Guid.NewGuid();
        var produtos = new List<Produto>
    {
        new Produto(Guid.NewGuid(), "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem1.jpg", categoriaId),
        new Produto(Guid.NewGuid(), "Suco", "Suco de laranja", 10m, "http://exemplo.com/imagem2.jpg", categoriaId)
    };

        mockGateway.Setup(gateway => gateway.GetProdutosByCategoriaAsync(categoriaId)).ReturnsAsync(produtos);

        // Act
        var resultado = await mockGateway.Object.GetProdutosByCategoriaAsync(categoriaId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public async Task ListarTodosProdutosAsync_DeveRetornarListaDeProdutos()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produtos = new List<Produto>
    {
        new Produto(Guid.NewGuid(), "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem1.jpg", Guid.NewGuid()),
        new Produto(Guid.NewGuid(), "Suco", "Suco de laranja", 10m, "http://exemplo.com/imagem2.jpg", Guid.NewGuid())
    };

        mockGateway.Setup(gateway => gateway.ListarTodosProdutosAsync()).ReturnsAsync(produtos);

        // Act
        var resultado = await mockGateway.Object.ListarTodosProdutosAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public async Task ListarProdutosByIdAsync_DeveRetornarProdutosQuandoIdsValidos()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produtoIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var produtos = new List<Produto>
    {
        new Produto(produtoIds[0], "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem1.jpg", Guid.NewGuid()),
        new Produto(produtoIds[1], "Suco", "Suco de laranja", 10m, "http://exemplo.com/imagem2.jpg", Guid.NewGuid())
    };

        mockGateway.Setup(gateway => gateway.ListarProdutosByIdAsync(It.IsAny<ICollection<Guid>>())).ReturnsAsync(produtos);

        // Act
        var resultado = await mockGateway.Object.ListarProdutosByIdAsync(produtoIds);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }


}
