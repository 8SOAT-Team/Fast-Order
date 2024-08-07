using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Tests.Entities;
public class ProdutoTest
{
    [Fact]
    public void DeveRetornarExceptionQuandoNomeForNulo()
    {
        //Arrange
        var nome = "";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "http://Imagemdeteste", Guid.NewGuid());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }
    [Fact]
    public void DeveRetornarExceptionQuandoNomeForMenorQue3Caracteres()
    {
        //Arrange
        var nome = "Te";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "http://Imagemdeteste", Guid.NewGuid());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void DeveRetornarExceptionQuandoImagemInvalida()
    {
        //Arrange
        var nome = "Batata Frita";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "Imagem de teste", Guid.NewGuid());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void DeveCriarProdutoComSucesso()
    {
        //Arrange
        var nome = "Teste";
        var descricao = "Teste";
        //Act
        var produto = new Produto(nome, descricao, 10, "https://imagemdeteste", Guid.NewGuid());
        //Assert
        Assert.NotNull(produto);
    }

    [Theory]
    [InlineData("Teste", "Teste", 10, "https://imagemdeteste1", "167e0817-036c-491d-841b-65926809db6d")]
    [InlineData("Teste2", "Teste2", 10, "http://imagemdeteste2", "129bef6d-3057-4c06-a33c-1792e5686b3b")]
    [InlineData("Teste3", "Teste3", 10, "http://imagemdeteste3", "e65e9a4f-c17c-4d2a-b537-c6d156349dc4")]
    public void DeveCriarProdutoComSucessoParametrizado(string nome, string descricao, decimal preco, string imagem, Guid categoriaId)
    {
        //Arrange
        //Act
        var produto = new Produto(nome, descricao, preco, imagem, categoriaId);
        //Assert
        Assert.NotNull(produto);
    }

    [Fact]
    public void DeveRetornarExceptionQuandoImagemForNula()
    {
        //Arrange
        var nome = "Teste";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "", Guid.NewGuid());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void DeveRetornarExceptionQuandoImagemForMenorQue3Caracteres()
    {
        //Arrange
        var nome = "Teste";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "Im", Guid.NewGuid());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void RenameTo_DadoQueONomeEhValido_DeveRenomear()
    {
        // Arrange
        var novoNome = "Novo Nome";
        var produto = new Produto("Nome Antigo", "Descricao", 10, "https://ImageUrl", Guid.NewGuid());

        // Act
        produto.RenameTo(novoNome);

        // Assert
        Assert.Equal(produto.Nome, novoNome);
    }
}
