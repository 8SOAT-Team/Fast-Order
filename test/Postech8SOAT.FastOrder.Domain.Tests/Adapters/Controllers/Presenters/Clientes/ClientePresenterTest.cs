using Postech8SOAT.FastOrder.Domain.Tests.Stubs.Pedidos;
using Postech8SOAT.FastOrder.Presenters.Clientes;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Presenters;

public class ClientePresenterTest
{
    [Fact]
    public void AdaptClienteIdentificado_ShouldReturnCorrectDto()
    {
        // Arrange
        var cliente = ClienteStubBuilder.Create();

        // Act
        var result = ClientePresenter.AdaptClienteIdentificado(cliente);

        // Assert
        Assert.Equal(cliente.Id, result.Id);
        Assert.Equal(cliente.Nome, result.Nome);
    }

    [Fact]
    public void AdaptCliente_ShouldReturnCorrectDto()
    {
        // Arrange
        var cliente = ClienteStubBuilder.Create();

        // Act
        var result = ClientePresenter.AdaptCliente(cliente);

        // Assert
        Assert.Equal(cliente.Id, result.Id);
        Assert.Equal(cliente.Nome, result.Nome);
        Assert.Equal(cliente.Email, result.Email);
        Assert.Equal(cliente.Cpf, result.Cpf);
    }
}