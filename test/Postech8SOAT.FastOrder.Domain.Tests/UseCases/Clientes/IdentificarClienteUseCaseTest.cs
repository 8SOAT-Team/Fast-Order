using Moq;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.UseCases.Clientes;

namespace Postech8SOAT.FastOrder.Domain.Tests.Abstractions.Clientes;
public class IdentificarClienteUseCaseTest
{

    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<IClienteGateway> _clienteGatewayMock;

    [Fact]
    public async Task Execute_DeveRetornarClienteQuandoCpfExistente()
    {
        // Arrange
        var cpf = new Cpf("12345678901");
        var clienteEsperado = new Cliente(Guid.NewGuid(), cpf, "Cliente Teste", "cliente@dominio.com");


        var mockLogger = new Mock<ILogger>();

        // Mock do IClienteGateway
        var mockClienteGateway = new Mock<IClienteGateway>();
        mockClienteGateway
            .Setup(gateway => gateway.GetClienteByCpfAsync(cpf))
            .ReturnsAsync(clienteEsperado);

        // Instância do UseCase com os mocks
        var useCase = new IdentificarClienteUseCaseTestWrapper(mockLogger.Object, mockClienteGateway.Object);

        // Act
        var resultado = await useCase.PublicExecute(cpf);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(clienteEsperado, resultado);
        mockClienteGateway.Verify(gateway => gateway.GetClienteByCpfAsync(cpf), Times.Once);
    }

    [Fact]
    public async Task Execute_DeveRetornarNullQuandoCpfNaoExistente()
    {
        // Arrange
        var cpf = new Cpf("12345678901");

        // Mock do ILogger
        var mockLogger = new Mock<ILogger>();

        // Mock do IClienteGateway
        var mockClienteGateway = new Mock<IClienteGateway>();
        mockClienteGateway
            .Setup(gateway => gateway.GetClienteByCpfAsync(cpf))
            .ReturnsAsync((Cliente?)null);

        // Instância do UseCase com os mocks
        var useCase = new IdentificarClienteUseCaseTestWrapper(mockLogger.Object, mockClienteGateway.Object);

        // Act
        var resultado = await useCase.PublicExecute(cpf);

        // Assert
        Assert.Null(resultado);
        mockClienteGateway.Verify(gateway => gateway.GetClienteByCpfAsync(cpf), Times.Once);
    }


}


public class IdentificarClienteUseCaseTestWrapper : IdentificarClienteUseCase
{
    public IdentificarClienteUseCaseTestWrapper(ILogger logger, IClienteGateway clienteGateway)
        : base(logger, clienteGateway)
    {
    }

    public Task<Cliente?> PublicExecute(Cpf cpf)
    {
        return Execute(cpf);
    }
}