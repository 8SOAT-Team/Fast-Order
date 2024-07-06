using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Exceptions;

namespace Postech8SOAT.FastOrder.Domain.Tests.Entities;
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
        Assert.Equal(cpf, cliente.Cpf);
        Assert.Equal(nome, cliente.Nome);
        Assert.Equal(email, cliente.Email);
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
        Assert.Throws<DomainExceptionValidation>(() => new Cliente(cpf, nome, email));
    }

    [Theory]
    [InlineData("1234567890", "Nome válido", "email@example.com")]
    [InlineData("12345678901234567890", "Nome válido", "email@example.com")]
    public void RetornaExcptionQuandoCPFInvalido(string cpf, string nome, string email)
    {
        // Act & Assert
        Assert.Throws<DomainExceptionValidation>(() => new Cliente(cpf, nome, email));
    }

    [Theory]
    [InlineData("12345678900", "Jo", "email@example.com")]
    [InlineData("12345678900", "Nome válido com mais de 100 caracteres Nome válido com mais de 100 caracteres Nome válido com mais de 100 caracteres Nome válido com mais de 100 caracteres", "email@example.com")]
    public void RetornaExceptionQuandoNomeTamanhoInvalido(string cpf, string nome, string email)
    {
        // Act & Assert
        Assert.Throws<DomainExceptionValidation>(() => new Cliente(cpf, nome, email));
    }

    [Theory]
    [InlineData("12345678900", "Nome válido", "")]
    [InlineData("12345678900", "Nome válido", "email")]
    public void RetornaExceptionQuandoEmailTamanhoInvalido(string cpf, string nome, string email)
    {
        // Act & Assert
        Assert.Throws<DomainExceptionValidation>(() => new Cliente(cpf, nome, email));
    }
       
    [Theory]
    [InlineData("123.456.789-00", "João da Silva", "joao@example.com")]
    [InlineData("987.654.321-09", "Maria Oliveira", "maria@example.com")]
    public void ValidaClienteComCPFComPontuacao(string cpf, string nome, string email)
    {
        // Act
        var cliente = new Cliente(cpf, nome, email);

        // Assert
        Assert.Equal("12345678900", cliente.Cpf); // O CPF deve ser armazenado sem pontuação
        Assert.Equal(nome, cliente.Nome);
        Assert.Equal(email, cliente.Email);
    }

    [Fact]
    public void AtualizaClienteValido()
    {
        // Arrange
        var cliente = new Cliente("12345678900", "João da Silva", "joao@example.com");
        string novoNome = "João Silva";
        string novoEmail = "joao.silva@example.com";

        // Act
        cliente.Update("12345678900", novoNome, novoEmail);

        // Assert
        Assert.Equal("12345678900", cliente.Cpf);
        Assert.Equal(novoNome, cliente.Nome);
        Assert.Equal(novoEmail, cliente.Email);
    }


}
