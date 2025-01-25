using Moq;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Abstractions.Gateways;

public class ICategoriaGatewayTest
{
    //Testes de mock
    [Fact]
    public async Task ObterTodasCategoriasAsync_DeveRetornarCategorias()
    {
        // Arrange
        var mockCategoriaGateway = new Mock<ICategoriaGateway>();
        mockCategoriaGateway.Setup(gateway => gateway.GetAllCategoriasAsync())
            .ReturnsAsync(new List<Categoria> { new Categoria(Guid.NewGuid(), "Categoria 1", "Categoria de teste") });

        var categoriaService = new CategoriaService(mockCategoriaGateway.Object);

        // Act
        var categorias = await categoriaService.ObterTodasCategoriasAsync();

        // Assert
        Assert.NotNull(categorias);
        Assert.Single(categorias);  // Verifica que há 1 categoria
    }

    [Fact]
    public async Task ObterCategoriaPorIdAsync_DeveRetornarCategoria()
    {
        // Arrange
        var mockCategoriaGateway = new Mock<ICategoriaGateway>();
        var id = Guid.NewGuid();
        mockCategoriaGateway.Setup(gateway => gateway.GetCategoriaByIdAsync(id))
            .ReturnsAsync(new Categoria(id, "Categoria 1", "Categoria de teste"));

        var categoriaService = new CategoriaService(mockCategoriaGateway.Object);

        // Act
        var categoria = await categoriaService.ObterCategoriaPorIdAsync(id);

        // Assert
        Assert.NotNull(categoria);
        Assert.Equal(id, categoria?.Id);
    }

    [Fact]
    public async Task ObterCategoriaPorIdAsync_DeveRetornarNullQuandoCategoriaNaoExistir()
    {
        // Arrange
        var mockCategoriaGateway = new Mock<ICategoriaGateway>();
        var id = Guid.NewGuid();  // Um ID inexistente
        mockCategoriaGateway.Setup(gateway => gateway.GetCategoriaByIdAsync(id))
            .ReturnsAsync((Categoria?)null);

        var categoriaService = new CategoriaService(mockCategoriaGateway.Object);

        // Act
        var categoria = await categoriaService.ObterCategoriaPorIdAsync(id);

        // Assert
        Assert.Null(categoria);
    }

    //Testes implementação
    [Fact]
    public async Task GetAllCategoriasAsync_DeveRetornarTodasCategorias()
    {
        // Arrange
        var categoriaGateway = new CategoriaGateway();

        // Act
        var categorias = await categoriaGateway.GetAllCategoriasAsync();

        // Assert
        Assert.NotNull(categorias);
        Assert.Equal(2, categorias.Count);  // Verifica se há 2 categorias
    }

    [Fact]
    public async Task GetCategoriaByIdAsync_DeveRetornarCategoriaQuandoExistir()
    {
        // Arrange
        var categoriaGateway = new CategoriaGateway();
        var idCategoriaExistente = categoriaGateway.GetAllCategoriasAsync().Result.First().Id;

        // Act
        var categoria = await categoriaGateway.GetCategoriaByIdAsync(idCategoriaExistente);

        // Assert
        Assert.NotNull(categoria);
        Assert.Equal(idCategoriaExistente, categoria?.Id);
    }

    [Fact]
    public async Task GetCategoriaByIdAsync_DeveRetornarNullQuandoCategoriaNaoExistir()
    {
        // Arrange
        var categoriaGateway = new CategoriaGateway();
        var idCategoriaInexistente = Guid.NewGuid();  // Um ID que não existe na lista

        // Act
        var categoria = await categoriaGateway.GetCategoriaByIdAsync(idCategoriaInexistente);

        // Assert
        Assert.Null(categoria);  // Verifica se a categoria não foi encontrada
    }

}

//mock
public class CategoriaService
{
    private readonly ICategoriaGateway _categoriaGateway;

    public CategoriaService(ICategoriaGateway categoriaGateway)
    {
        _categoriaGateway = categoriaGateway;
    }

    public async Task<ICollection<Categoria>> ObterTodasCategoriasAsync()
    {
        return await _categoriaGateway.GetAllCategoriasAsync();
    }

    public async Task<Categoria?> ObterCategoriaPorIdAsync(Guid id)
    {
        return await _categoriaGateway.GetCategoriaByIdAsync(id);
    }
}



public class CategoriaGateway : ICategoriaGateway
{
    private readonly List<Categoria> _categorias;

    public CategoriaGateway()
    {
        // Inicializa uma lista de categorias para simular um banco de dados.
        _categorias = new List<Categoria>
        {
            new Categoria(Guid.NewGuid(), "Categoria 1", "Categoria descrição"),
            new Categoria(Guid.NewGuid(), "Categoria 2", "Categoria 2 descrição")
        };
    }

    public Task<ICollection<Categoria>> GetAllCategoriasAsync()
    {
        return Task.FromResult((ICollection<Categoria>)_categorias);
    }

    public Task<Categoria?> GetCategoriaByIdAsync(Guid id)
    {
        var categoria = _categorias.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(categoria);
    }
}