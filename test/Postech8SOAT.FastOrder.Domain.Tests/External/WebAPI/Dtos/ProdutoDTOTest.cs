using System.ComponentModel.DataAnnotations;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.WebAPI.Dtos;

public class ProdutoDTOTest
{
    [Fact]
    public void ProdutoDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Produto Teste";
        var descricao = "Descrição do Produto Teste";
        var preco = 99.99m;
        var categoriaId = Guid.NewGuid();
        var imagem = "imagem.jpg";

        // Act
        var dto = new ProdutoDTO
        {
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            CategoriaId = categoriaId,
            Imagem = imagem
        };
        dto.SetId(id);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(nome, dto.Nome);
        Assert.Equal(descricao, dto.Descricao);
        Assert.Equal(preco, dto.Preco);
        Assert.Equal(categoriaId, dto.CategoriaId);
        Assert.Equal(imagem, dto.Imagem);
    }

    [Fact]
    public void ProdutoDTO_ShouldRequireNome()
    {
        // Arrange
        var dto = new ProdutoDTO
        {
            Descricao = "Descrição do Produto Teste",
            Preco = 99.99m,
            CategoriaId = Guid.NewGuid(),
            Imagem = "imagem.jpg"
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
    public void ProdutoDTO_ShouldRequireDescricao()
    {
        // Arrange
        var dto = new ProdutoDTO
        {
            Nome = "Produto Teste",
            Preco = 99.99m,
            CategoriaId = Guid.NewGuid(),
            Imagem = "imagem.jpg"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "A descrição é requerida.");
    }

    [Fact]
    public void ProdutoDTO_ShouldRequirePreco()
    {
        // Arrange
        var dto = new ProdutoDTO
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do Produto Teste",
            CategoriaId = Guid.NewGuid(),
            Imagem = "imagem.jpg"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(dto, null, null);
        var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage == "O preço deve ser maior que zero.");
    }
}