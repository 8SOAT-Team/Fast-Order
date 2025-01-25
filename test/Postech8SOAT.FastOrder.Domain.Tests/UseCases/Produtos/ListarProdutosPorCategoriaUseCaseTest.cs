using Moq;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Produtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using CleanArch.UseCase.Faults;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Produtos;
public class ListarProdutosPorCategoriaUseCaseTest
{
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<IProdutoGateway> _produtoGatewayMock;
    private readonly ListarProdutoPorCategoriaTest _useCase;

    public ListarProdutosPorCategoriaUseCaseTest()
    {
        _loggerMock = new Mock<ILogger>();
        _produtoGatewayMock = new Mock<IProdutoGateway>();
        _useCase = new ListarProdutoPorCategoriaTest(_loggerMock.Object, _produtoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_CategoriaIdVazio_RetornaErro()
    {
        // Arrange
        var categoriaId = Guid.Empty;

        // Act
        var result = await _useCase.Execute(categoriaId);

        // Assert
        Assert.Null(result); 
        _produtoGatewayMock.Verify(pg => pg.GetProdutosByCategoriaAsync(It.IsAny<Guid>()), Times.Never);
        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("CategoriaId não pode ser vazio", useCaseErrors.FirstOrDefault().Description);
    }

    [Fact]
    public async Task Execute_ProdutosExistemParaCategoria_RetornaListaDeProdutos()
    {
        // Arrange
        var categoriaId = Guid.NewGuid();
        var produto1 = new Produto("Lanche1", "Lanche de bacon2", 100.00m, "http://endereco/imagens/img.jpg", Guid.NewGuid());
        var produto2 = new Produto("Lanche2", "Lanche de bacon2", 150.00m, "http://endereco/imagens/img.jpg", Guid.NewGuid());

        var produtos = new List<Produto> { produto1, produto2 };

        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(categoriaId)).ReturnsAsync(produtos);

        // Act
        var result = await _useCase.Execute(categoriaId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _produtoGatewayMock.Verify(pg => pg.GetProdutosByCategoriaAsync(categoriaId), Times.Once);
    }

    [Fact]
    public async Task Execute_NaoExistemProdutosParaCategoria_RetornaListaVazia()
    {
        // Arrange
        var categoriaId = Guid.NewGuid(); 
        var produtos = new List<Produto>(); 

        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(categoriaId)).ReturnsAsync(produtos);

        // Act
        var result = await _useCase.Execute(categoriaId);

        // Assert
        Assert.NotNull(result); 
        Assert.Empty(result);
        _produtoGatewayMock.Verify(pg => pg.GetProdutosByCategoriaAsync(categoriaId), Times.Once);
    }

    [Fact]
    public async Task Execute_ErroNoGateway_ExecutaLogger()
    {
        // Arrange
        var categoriaId = Guid.NewGuid(); 
        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(categoriaId)).ThrowsAsync(new Exception("Erro ao acessar o repositório"));

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _useCase.Execute(categoriaId));

        // Assert
        Assert.Equal("Erro ao acessar o repositório", exception.Message);
    }
}

public class ListarProdutoPorCategoriaTest : ListarProdutoPorCategoriaUseCase
{
    public ListarProdutoPorCategoriaTest(ILogger logger, IProdutoGateway produtoGateway)
        : base(logger, produtoGateway) { }

    public new Task<ICollection<Produto>?> Execute(Guid command)
    {
        return base.Execute(command);
    }

}