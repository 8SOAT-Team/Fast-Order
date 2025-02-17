using System.ComponentModel.DataAnnotations;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.WebAPI.Dtos;

public class CategoriaDtoTest
{
    [Fact]
    public void CategoriaDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Categoria Teste";
        var descricao = "Descrição Teste";

        // Act
        var dto = new CategoriaDTO(id, nome, descricao);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(nome, dto.Nome);
        Assert.Equal(descricao, dto.Descricao);
    }

    [Fact]
    public void CategoriaDTO_ShouldRequireNome()
    {
        // Arrange
        var dto = new CategoriaDTO(Guid.NewGuid(), null!, "Descrição Teste");

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "O nome é requerido.");
    }

    [Fact]
    public void CategoriaDTO_ShouldRequireDescricao()
    {
        // Arrange
        var dto = new CategoriaDTO(Guid.NewGuid(), "Categoria Teste", null!);

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "O descrição é requerido.");
    }

    [Fact]
    public void CategoriaDTO_Nome_ShouldHaveMinLength()
    {
        // Arrange
        var dto = new CategoriaDTO(Guid.NewGuid(), "Ca", "Descrição Teste");

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage.Contains("The field Nome must be a string or array type with a minimum length of '3'."));
    }

    [Fact]
    public void CategoriaDTO_Descricao_ShouldHaveMinLength()
    {
        // Arrange
        var dto = new CategoriaDTO(Guid.NewGuid(), "Categoria Teste", "De");

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage.Contains("The field Descricao must be a string or array type with a minimum length of '3'."));
    }
}