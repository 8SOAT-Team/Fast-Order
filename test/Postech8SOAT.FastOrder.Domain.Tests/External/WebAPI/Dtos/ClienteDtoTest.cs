using System.ComponentModel.DataAnnotations;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.WebAPI.Dtos;

public class ClienteDtoTest
{
    [Fact]
    public void ClienteDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cpf = "123.456.789-00";
        var nome = "Cliente Teste";
        var email = "cliente@teste.com";

        // Act
        var dto = new ClienteDTO
        {
            Cpf = cpf,
            Nome = nome,
            Email = email
        };
        dto.SetId(id);

        // Assert
        Assert.Equal(id, dto.ClienteId);
        Assert.Equal(cpf, dto.Cpf);
        Assert.Equal(nome, dto.Nome);
        Assert.Equal(email, dto.Email);
    }

    [Fact]
    public void ClienteDTO_ShouldRequireCpf()
    {
        // Arrange
        var dto = new ClienteDTO
        {
            Nome = "Cliente Teste",
            Email = "cliente@teste.com"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "CPF deve estar preenchido.");
    }

    [Fact]
    public void ClienteDTO_ShouldRequireNome()
    {
        // Arrange
        var dto = new ClienteDTO
        {
            Cpf = "123.456.789-00",
            Email = "cliente@teste.com"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "O nome é requerido.");
    }

    [Fact]
    public void ClienteDTO_ShouldRequireEmail()
    {
        // Arrange
        var dto = new ClienteDTO
        {
            Cpf = "123.456.789-00",
            Nome = "Cliente Teste"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "O email deve ser informado");
    }

    [Fact]
    public void ClienteDTO_Cpf_ShouldHaveCorrectFormat()
    {
        // Arrange
        var dto = new ClienteDTO
        {
            Cpf = "12345678900",
            Nome = "Cliente Teste",
            Email = "cliente@teste.com"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "CPF inválido");
    }

    [Fact]
    public void ClienteDTO_Email_ShouldHaveCorrectFormat()
    {
        // Arrange
        var dto = new ClienteDTO
        {
            Cpf = "123.456.789-00",
            Nome = "Cliente Teste",
            Email = "cliente"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "Formato do email inválido");
    }
}