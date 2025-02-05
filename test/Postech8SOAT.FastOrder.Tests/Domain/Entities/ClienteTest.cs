using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.Tests.Domain.Entities;
public class ClienteTest
{
    [Fact]
    public void RetornaUmClienteValido()
    {
        // Arrange
        string cpf = "12345678900";
        string nome = "João da Silva";
        string email = "joao@example.com";

        // Act
        var cliente = new Cliente(cpf, nome, email);

        // Assert 
        Assert.Equal(cpf, cliente.Cpf.Value);
        Assert.Equal(nome, cliente.Nome);
        Assert.Equal(email, cliente.Email.Address);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void RetornaThrowsExceptionQuandoCPFeHNulo(string cpf)
    {
        // Arrange
        string nome = "João da Silva";
        string email = "joao@example.com";

        // Act & Assert
        Assert.ThrowsAny<DomainExceptionValidation>(() => new Cliente(cpf, nome, email));
    }

    [Theory]
    [InlineData("1234567890", "Nome válido", "email@example.com")]
    [InlineData("12345678901234567890", "Nome válido", "email@example.com")]
    public void RetornaExceptionQuandoCPFInvalido(string cpf, string nome, string email)
    {
        // Act & Assert
        Assert.ThrowsAny<DomainExceptionValidation>(() => new Cliente(cpf, nome, email));
    }

    [Theory]
    [InlineData("12345678900", "Jo", "email@example.com")]
    [InlineData("12345678900", "Nome válido com mais de 100 caracteres Nome válido com mais de 100 caracteres Nome válido com mais de 100 caracteres Nome válido com mais de 100 caracteres", "email@example.com")]
    public void RetornaExceptionQuandoNomeTamanhoInvalido(string cpf, string nome, string email)
    {
        // Act & Assert
        Assert.Throws<DomainExceptionValidation>(() => new Cliente(cpf, nome, email));
    }

    [Fact]
    public void ChangeNome_DadoQueONovoNomeEhValido_DeveAlterar()
    {
        // Arrange
        var cliente = new Cliente("12345678900", "João da Silva", "joao@example.com");
        string novoNome = "João Silva";

        // Act
        cliente.ChangeNome(novoNome);

        // Assert
        Assert.Equal(novoNome, cliente.Nome);
    }

    [Fact]
    public void ChangeEmail_DadoQueONovoEmailEhValido_DeveAlterar()
    {
        // Arrange
        var cliente = new Cliente("12345678900", "João da Silva", "joao@example.com");
        var novoEmail = new EmailAddress("joao.da.silva@example.com");

        // Act
        cliente.ChangeEmail(novoEmail);

        // Assert
        Assert.Equal(novoEmail, cliente.Email);
    }
}
