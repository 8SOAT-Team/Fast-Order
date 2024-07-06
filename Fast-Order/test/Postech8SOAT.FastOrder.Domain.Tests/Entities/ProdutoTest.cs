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
        Action act = () => new Produto(nome, descricao, 10, "Imagem de teste");
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
        Action act = () => new Produto(nome, descricao, 10, "Imagem de teste");
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
        var produto = new Produto(nome, descricao, 10, "Imagem de teste");
        //Assert
        Assert.NotNull(produto);
    }

    [Theory]
    [InlineData("Teste", "Teste", 10, "Imagem de teste 1")]
    [InlineData("Teste2", "Teste2", 10, "Imagem de teste 2")]
    [InlineData("Teste3", "Teste3", 10, "Imagem de teste 3")]
    public void DeveCriarProdutoComSucessoParametrizado(string nome, string descricao, decimal preco, string imagem)
    {
        //Arrange
        //Act
        var produto = new Produto(nome, descricao, preco, imagem);
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
        Action act = () => new Produto(nome, descricao, 10, "");
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
        Action act = () => new Produto(nome, descricao, 10, "Im");
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }
}
