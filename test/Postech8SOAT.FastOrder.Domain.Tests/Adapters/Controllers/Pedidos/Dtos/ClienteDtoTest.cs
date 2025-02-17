using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Pedidos.Dtos;

public class ClienteDtoTest
{
    [Fact]
    public void ClienteDTO_ShouldInitializeCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "John Doe";
        var email = "john.doe@example.com";
        var cpf = "12345678901";

        // Act
        var dto = new ClienteDTO
        {
            Id = id,
            Nome = nome,
            Email = email,
            Cpf = cpf
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(nome, dto.Nome);
        Assert.Equal(email, dto.Email);
        Assert.Equal(cpf, dto.Cpf);
    }

    [Fact]
    public void ClienteDTO_ShouldHandleNullValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        string? nome = null;
        string? email = null;
        string? cpf = null;

        // Act
        var dto = new ClienteDTO
        {
            Id = id,
            Nome = nome,
            Email = email,
            Cpf = cpf
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Null(dto.Nome);
        Assert.Null(dto.Email);
        Assert.Null(dto.Cpf);
    }
}