using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Tests.Domain.Entities;
public class CategoriaTest
{

    [Fact]
    public void DeveRetornarExceptionQuandoNomeForNulo()
    {
        //Arrange
        var nome = "";
        var descricao = "Teste";
        //Act
        Action act = () => new Categoria(nome, descricao);
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
        Action act = () => new Categoria(nome, descricao);
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void DeveCriarCategoriaComSucesso()
    {
        //Arrange
        var nome = "Teste";
        var descricao = "Teste";
        //Act
        var categoria = new Categoria(nome, descricao);
        //Assert
        Assert.NotNull(categoria);
    }

    [Theory]
    [InlineData("Teste", "Teste")]
    [InlineData("Teste2", "Teste2")]
    [InlineData("Teste3", "Teste3")]
    public void DeveCriarCategoriaComSucessoParametrizado(string nome, string descricao)
    {
        //Arrange
        //Act
        var categoria = new Categoria(nome, descricao);
        //Assert
        Assert.NotNull(categoria);
    }




}
