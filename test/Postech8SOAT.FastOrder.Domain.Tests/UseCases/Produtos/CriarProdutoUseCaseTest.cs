using Moq;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Produtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;
using CleanArch.UseCase.Faults;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Produtos;
public class CriarProdutoUseCaseTest
{
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<ICategoriaGateway> _categoriaGatewayMock;
    private readonly Mock<IProdutoGateway> _produtoGatewayMock;
    private readonly CriarProdutoTest _useCase;

    public CriarProdutoUseCaseTest()
    {
        _loggerMock = new Mock<ILogger>();
        _categoriaGatewayMock = new Mock<ICategoriaGateway>();
        _produtoGatewayMock = new Mock<IProdutoGateway>();
        _useCase = new CriarProdutoTest(_loggerMock.Object, _categoriaGatewayMock.Object, _produtoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_CategoriaNaoEncontrada_RetornaErro()
    {
        // Arrange
        var novoProdutoDto = new NovoProdutoDTO
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do produto",
            Preco = 10.0m,
            Imagem = "url_da_imagem",
            CategoriaId = Guid.NewGuid()
        };

        _categoriaGatewayMock.Setup(cg => cg.GetCategoriaByIdAsync(novoProdutoDto.CategoriaId)).ReturnsAsync((Categoria?)null);

        // Act
        var result = await _useCase.Execute(novoProdutoDto);

        // Assert
        Assert.Null(result);
        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("CategoriaId não existe", useCaseErrors.FirstOrDefault().Description);
        _produtoGatewayMock.Verify(pg => pg.GetProdutosByCategoriaAsync(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task Execute_ProdutoJaExiste_RetornaErro()
    {
        // Arrange
        var categoriaId = Guid.NewGuid();
        var novoProdutoDto = new NovoProdutoDTO
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do produto",
            Preco = 10.0m,
            Imagem = "http://endereco/imagens/img.jpg",
            CategoriaId = categoriaId
        };

        var categoria = new Categoria(categoriaId, "Categoria", "Categoria Teste");
        var produtosNaCategoria = new List<Produto>
            {
                new Produto(Guid.NewGuid(), "Produto Teste", "Descrição de produto existente", 20.0m, "http://endereco/imagens/img.jpg", categoriaId)
            };

        _categoriaGatewayMock.Setup(cg => cg.GetCategoriaByIdAsync(categoriaId)).ReturnsAsync(categoria);
        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(categoriaId)).ReturnsAsync(produtosNaCategoria);

        // Act
        var result = await _useCase.Execute(novoProdutoDto);

        // Assert
        Assert.Null(result);
        IReadOnlyCollection<UseCaseError> useCaseErrors = _useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Já existe esse produto cadastrado para essa categoria.", useCaseErrors.FirstOrDefault().Description);
        _produtoGatewayMock.Verify(pg => pg.CreateProdutoAsync(It.IsAny<Produto>()), Times.Never); 
    }

    [Fact]
    public async Task Execute_ProdutoCriadoComSucesso_RetornaProdutoCriado()
    {
        // Arrange
        var categoriaId = Guid.NewGuid();
        var novoProdutoDto = new NovoProdutoDTO
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do produto",
            Preco = 10.0m,
            Imagem = "http://endereco/imagens/img.jpg",
            CategoriaId = categoriaId
        };

        var categoria = new Categoria(categoriaId, "Categoria", "Categoria Teste");
        var produtosNaCategoria = new List<Produto>(); 

        var produtoCriado = new Produto(Guid.NewGuid(), "Produto Teste", "Descrição do produto", 10.0m, "http://endereco/imagens/img.jpg", categoriaId);

        _categoriaGatewayMock.Setup(cg => cg.GetCategoriaByIdAsync(categoriaId)).ReturnsAsync(categoria);
        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(categoriaId)).ReturnsAsync(produtosNaCategoria);
        _produtoGatewayMock.Setup(pg => pg.CreateProdutoAsync(It.IsAny<Produto>())).ReturnsAsync(produtoCriado);

        // Act
        var result = await _useCase.Execute(novoProdutoDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(novoProdutoDto.Nome, result.Nome); 
        _produtoGatewayMock.Verify(pg => pg.CreateProdutoAsync(It.IsAny<Produto>()), Times.Once);
    }

    [Fact]
    public async Task Execute_ErroAoCriarProduto_RetornaNull()
    {
        // Arrange
        var categoriaId = Guid.NewGuid();
        var novoProdutoDto = new NovoProdutoDTO
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do produto",
            Preco = 10.0m,
            Imagem = "http://endereco/imagens/img.jpg",
            CategoriaId = categoriaId
        };

        var categoria = new Categoria(categoriaId, "Categoria", "Categoria Teste");
        var produtosNaCategoria = new List<Produto>();

        _categoriaGatewayMock.Setup(cg => cg.GetCategoriaByIdAsync(categoriaId)).ReturnsAsync(categoria);
        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(categoriaId)).ReturnsAsync(produtosNaCategoria);
        _produtoGatewayMock.Setup(pg => pg.CreateProdutoAsync(It.IsAny<Produto>())).ThrowsAsync(new Exception("Erro ao salvar produto"));

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _useCase.Execute(novoProdutoDto));

        // Assert
        Assert.Equal("Erro ao salvar produto", exception.Message);
    }
}

public class CriarProdutoTest : CriarProdutoUseCase
{
    public CriarProdutoTest(ILogger logger, ICategoriaGateway categoriaGateway, IProdutoGateway produtoGateway)
        : base(logger, categoriaGateway, produtoGateway) { }

    public new Task<Produto?> Execute(NovoProdutoDTO command)
    {
        return base.Execute(command);
    }

}