using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Tests.Domain.UseCases.Produtos.Dtos;
public class ProdutoCategoriaDTOTest
{
    [Fact]
    public void DeveCriarProdutoCategoriaDTO_ComValoresCorretos()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Categoria Produto";
        var descricao = "Descrição da Categoria Produto";

        // Act
        var produtoCategoria = new ProdutoCategoriaDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao
        };

        // Assert
        Assert.Equal(id, produtoCategoria.Id);
        Assert.Equal(nome, produtoCategoria.Nome);
        Assert.Equal(descricao, produtoCategoria.Descricao);
    }

    [Fact]
    public void DeveCriarProdutoCategoriaDTO_ComNomeVazio()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = ""; // Nome vazio
        var descricao = "Descrição da Categoria com Nome Vazio";

        // Act
        var produtoCategoria = new ProdutoCategoriaDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao
        };

        // Assert
        Assert.Equal(id, produtoCategoria.Id);
        Assert.Equal(nome, produtoCategoria.Nome);
        Assert.Equal(descricao, produtoCategoria.Descricao);
    }

    [Fact]
    public void DeveCriarProdutoCategoriaDTO_ComDescricaoVazia()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Categoria Produto com Descrição Vazia";
        var descricao = ""; // Descrição vazia

        // Act
        var produtoCategoria = new ProdutoCategoriaDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao
        };

        // Assert
        Assert.Equal(id, produtoCategoria.Id);
        Assert.Equal(nome, produtoCategoria.Nome);
        Assert.Equal(descricao, produtoCategoria.Descricao);
    }

    [Fact]
    public void DeveCriarProdutoCategoriaDTO_ComNomeEDescricaoLongos()
    {
        // Arrange
        var nome = new string('A', 100);
        var descricao = new string('B', 200);
        var id = Guid.NewGuid();

        // Act
        var produtoCategoria = new ProdutoCategoriaDTO
        {
            Id = id,
            Nome = nome,
            Descricao = descricao
        };

        // Assert
        Assert.Equal(id, produtoCategoria.Id);
        Assert.Equal(nome, produtoCategoria.Nome);
        Assert.Equal(descricao, produtoCategoria.Descricao);
    }

    [Fact]
    public void DeveSerDiferenteQuandoProdutoCategoriaDTOForDeValoresDiferentes()
    {
        // Arrange
        var id1 = Guid.NewGuid();
        var nome1 = "Categoria B";
        var descricao1 = "Descrição da Categoria B";

        var id2 = Guid.NewGuid();
        var nome2 = "Categoria C";
        var descricao2 = "Descrição da Categoria C";

        var categoria1 = new ProdutoCategoriaDTO
        {
            Id = id1,
            Nome = nome1,
            Descricao = descricao1
        };

        var categoria2 = new ProdutoCategoriaDTO
        {
            Id = id2,
            Nome = nome2,
            Descricao = descricao2
        };

        // Act and Assert
        Assert.NotEqual(categoria1, categoria2);
    }
}
