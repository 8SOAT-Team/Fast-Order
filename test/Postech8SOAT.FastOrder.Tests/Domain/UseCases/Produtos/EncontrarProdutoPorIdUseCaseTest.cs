using Moq;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Produtos;
using CleanArch.UseCase.Faults;

namespace Postech8SOAT.FastOrder.Tests.Domain.UseCases.Produtos;
public class EncontrarProdutoPorIdUseCaseTest
{
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<IProdutoGateway> _produtoGatewayMock;
    private readonly EncontrarProdutoPorIdTest _useCase;

    public EncontrarProdutoPorIdUseCaseTest()
    {
        _loggerMock = new Mock<ILogger>();
        _produtoGatewayMock = new Mock<IProdutoGateway>();
        _useCase = new EncontrarProdutoPorIdTest(_loggerMock.Object, _produtoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_IdVazio_RetornaErro()
    {
        // Arrange
        var produtoId = Guid.Empty; 

        // Act
        var result = await _useCase.Execute(produtoId); 

        // Assert
        Assert.Null(result);

        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Id do produto não pode ser vazio.", useCaseErrors.FirstOrDefault().Description);
        _produtoGatewayMock.Verify(pg => pg.GetProdutoByIdAsync(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task Execute_ProdutoExistente_RetornaProduto()
    {
        // Arrange
        var produtoId = Guid.NewGuid();
        var produto = new Produto(produtoId, "Lanche", "Lanche de bacon", 100.00m, "http://endereco/imagens/img.jpg", Guid.NewGuid());


        // Mock para retornar o produto
        _produtoGatewayMock.Setup(pg => pg.GetProdutoByIdAsync(produtoId)).ReturnsAsync(produto);

        // Act
        var result = await _useCase.Execute(produtoId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(produtoId, result.Id);
        Assert.Equal("Lanche", result.Nome);
        _produtoGatewayMock.Verify(pg => pg.GetProdutoByIdAsync(produtoId), Times.Once);
    }

    [Fact]
    public async Task Execute_ProdutoNaoEncontrado_RetornaNull()
    {
        // Arrange
        var produtoId = Guid.NewGuid();
        Produto? produto = null;  
        _produtoGatewayMock.Setup(pg => pg.GetProdutoByIdAsync(produtoId)).ReturnsAsync(produto);

        // Act
        var result = await _useCase.Execute(produtoId);

        // Assert
        Assert.Null(result); 
        _produtoGatewayMock.Verify(pg => pg.GetProdutoByIdAsync(produtoId), Times.Once);
    }

    [Fact]
    public async Task Execute_ErroNoGateway_ExecutaLogger()
    {
        // Arrange
        var produtoId = Guid.NewGuid();
        _produtoGatewayMock.Setup(pg => pg.GetProdutoByIdAsync(produtoId)).ThrowsAsync(new Exception("Erro ao acessar o repositório"));

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _useCase.Execute(produtoId));

        // Assert
        Assert.Equal("Erro ao acessar o repositório", exception.Message);
    }

}

public class EncontrarProdutoPorIdTest : EncontrarProdutoPorIdUseCase
{
    public EncontrarProdutoPorIdTest(ILogger logger, IProdutoGateway produtoGateway)
        : base(logger, produtoGateway) { }

    public new Task<Produto?> Execute(Guid id)
    {
        return base.Execute(id);
    }

}