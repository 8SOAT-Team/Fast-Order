using Postech8SOAT.FastOrder.Domain.Exceptions;
using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.Domain.Tests.ValueObjects;

public class EmailAddressTest
{
    [Theory]
    [InlineData("meu.email@dominio.com.br")]
    [InlineData("meu.email@dominio.com")]
    [InlineData("meu_email-com-caracteres_3speciais@dominio.com")]
    public void DadoQueOEmailEstaEmFormatoCorreto_Deve_RetornarUmEmailAddress(string email)
    {
        // Act
        var emailAddress =  new EmailAddress(email);

        // Assert
        Assert.Equal(email, emailAddress.Address);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("email")]
    [InlineData("email.invalido@")]
    [InlineData("email.invalido@dominio")]
    [InlineData("email.invalido@dominio.")]
    public void DadoQueOEmailNaoEstaEmFormatoValido_Deve_LancarUmaException(string emailInvalido)
    {
        // Arrange
        const string mensagemDeErroEsperada = "Email não está em um formato válido.";

        // Act
        var emailAddress = () => new EmailAddress(emailInvalido);

        // Assert
        var exception = Assert.Throws<InvalidEmailArgumentException>(emailAddress);
        Assert.Equal(mensagemDeErroEsperada, exception.Message);
    }

    [Fact]
    public void ToString_DadoQueOEmailEstaEmFormatoCorreto_Deve_RetornarValorCorreto()
    {
        // Arrange
        var textEmail = "meu.email@dominio.com.br";

        // Act
        var emailAddress =  new EmailAddress(textEmail);

        // Assert
        Assert.Equal(textEmail, emailAddress.ToString());
    }

    [Fact]
    public void ToString_DadoQueOsEmailsSaoIguais_Deve_RetornarVerdadeiro()
    {
        // Arrange
        var textEmail = "meu.email@dominio.com.br";

        // Act
        var primeiroEmailAddress =  new EmailAddress(textEmail);
        var segundoEmailAddress =  new EmailAddress(textEmail);

        // Assert
        Assert.Equal(primeiroEmailAddress, segundoEmailAddress);
    }

    [Fact]
    public void GetHashCode_DadoQueOsEmailsSaoIguais_Deve_RetornarHashCodesIguais()
    {
        // Arrange
        var textEmail = "meu.email@dominio.com.br";

        // Act
        var primeiroHashCode =  new EmailAddress(textEmail).GetHashCode();
        var segundoHashCode =  new EmailAddress(textEmail).GetHashCode();

        // Assert
        Assert.Equal(primeiroHashCode, segundoHashCode);
    }

    [Fact]
    public void GetHashCode_DadoQueOsEmailsSaoDiferentes_Deve_RetornarHashCodesDiferentes()
    {
        // Arrange
        var primeiroEmail = "meu.email@dominio.com.br";
        var segundoEmail = "meu.email@dominio.com";

        // Act
        var primeiroHashCode =  new EmailAddress(primeiroEmail).GetHashCode();
        var segundoHashCode =  new EmailAddress(segundoEmail).GetHashCode();

        // Assert
        Assert.NotEqual(primeiroHashCode, segundoHashCode);
    }
}
