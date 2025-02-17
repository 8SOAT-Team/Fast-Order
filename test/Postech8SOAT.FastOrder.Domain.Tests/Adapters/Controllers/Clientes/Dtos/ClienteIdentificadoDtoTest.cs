using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;


public class ClienteIdentificadoDtoTest
{

    [Fact]
    public void ClienteIdentificadoDto_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Test Name";

        // Act
        var dto = new ClienteIdentificadoDto(id, nome);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(nome, dto.Nome);
    }

    [Fact]
    public void ClienteIdentificadoDto_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var dto = new ClienteIdentificadoDto(default, default);

        // Assert
        Assert.Equal(default(Guid), dto.Id);
        Assert.Equal(default(string), dto.Nome);
    }
}