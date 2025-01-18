using Postech8SOAT.FastOrder.Domain.Exceptions;
using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.Domain.Tests.ValueObjects;

public sealed class CpfTest
{
    [Theory]
    [InlineData("11122233396")]
    [InlineData("111.222.333-96")]
    public void Cpf_DadoQueAEntradaEUmCpfValido_DeveRetornarUmaNovaInstancia(string cpfInput)
    {
        // Act
        var cpf = new Cpf(cpfInput);

        // Assert
        Assert.NotNull(cpf);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Cpf_DadoQueAEntradaEhEmpty_DeveLancarExcecao(string cpfInput)
    {
        // Act
        var act = () => new Cpf(cpfInput);

        // Assert
        var exception = Assert.Throws<InvalidArgumentException>(act);
        Assert.Equal("Cpf é obrigatório.", exception.Message);
    }

    [Theory]
    [InlineData("1112223339")]
    [InlineData("111222333967")]
    public void Cpf_DadoQueOComprimentoDaEntradaNaoEhOnze_DeveLancarExcecao(string cpfInput)
    {
        // Act
        var act = () => new Cpf(cpfInput);

        // Assert
        var exception = Assert.Throws<InvalidArgumentException>(act);
        Assert.Equal("Cpf deve conter 11 dígitos.", exception.Message);
    }

    [Fact]
    public void GetSanitized_DadoQueOValorInicialContemMascara_Deve_RetornarApenasOsDigitos()
    {
        // Arrange
        var initialValue = "111.222.333-96";
        var expectedValue = "11122233396";

        var cpf = new Cpf(initialValue);

        // Act
        var sanitizedValue = cpf.GetSanitized();

        // Assert
        Assert.Equal(expectedValue, sanitizedValue);
    }

    [Fact]
    public void GetHashCode_DadoQueAsDuasInstanciasTemValoresIguais_Deve_RetornarOMesmoHashCode()
    {
        // Arrange
        const string cpfValue = "111.222.333-96";
        var cpf = new Cpf(cpfValue);
        var cpfCopy = new Cpf(cpfValue);

        // Act
        var cpfHashCode = cpf.GetHashCode();
        var cpfCopyHashCode = cpfCopy.GetHashCode();

        // Assert
        Assert.Equal(cpfHashCode, cpfCopyHashCode);
    }

    [Fact]
    public void GetHashCode_DadoQueAsDuasInstanciasTemValoresDiferentes_Deve_RetornaCodigosDiferentes()
    {
        // Arrange
        const string cpfValue = "111.222.333-96";
        const string cpfCopyValue = "111.222.333-00";
        var cpf = new Cpf(cpfValue);
        var cpfCopy = new Cpf(cpfCopyValue);

        // Act
        var cpfHashCode = cpf.GetHashCode();
        var cpfCopyHashCode = cpfCopy.GetHashCode();

        // Assert
        Assert.NotEqual(cpfHashCode, cpfCopyHashCode);
    }

    [Fact]
    public void ToString_DadoQueEstaPreenchido_DeveRetornarOValorAtual()
    {
        // Arrange
        const string cpfValue = "111.222.333-96";
        var cpf = new Cpf(cpfValue);

        // Act
        var cpfToString = cpf.ToString();

        // Assert
        Assert.Equal(cpfValue, cpfToString);
    }
    
    
    [Theory]
    [InlineData("11122233396")]
    [InlineData("111.222.333-96")]
    public void TryCreate_QuandoDocumentoForValido_DeveRetornarTrueECpf(string cpfInput)
    {
        // Act
        var result = Cpf.TryCreate(cpfInput, out var cpf);

        // Assert
        Assert.True(result);
        Assert.NotNull(cpf);
        Assert.Equal(cpfInput, cpf.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("1112223339")]
    [InlineData("111222333967")]
    public void TryCreate_QuandoDocumentoForInvalido_DeveRetornarFalseECpfNulo(string cpfInput)
    {
        // Act
        var result = Cpf.TryCreate(cpfInput, out var cpf);

        // Assert
        Assert.False(result);
        Assert.Null(cpf);
    }
}
