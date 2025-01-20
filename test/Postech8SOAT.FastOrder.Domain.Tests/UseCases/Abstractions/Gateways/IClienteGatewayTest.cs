using Moq;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Abstractions.Gateways;
public class IClienteGatewayTest
{
    [Fact]
    public async Task GetClienteByCpfAsync_DeveRetornarClienteQuandoCpfExistir()
    {
        // Arrange
        var clienteGateway = new ClienteGateway();
        var cpf = new Cpf("12345678901");
        var cliente = new Cliente(cpf, "João Silva", new EmailAddress("teste@gmail.com"));
        await clienteGateway.InsertCliente(cliente);

        // Act
        var clienteRetornado = await clienteGateway.GetClienteByCpfAsync(cpf);

        // Assert
        Assert.NotNull(clienteRetornado);
        Assert.Equal(cliente.Cpf, clienteRetornado?.Cpf);
    }

    [Fact]
    public async Task GetClienteByCpfAsync_DeveRetornarNullQuandoCpfNaoExistir()
    {
        // Arrange
        var clienteGateway = new ClienteGateway();
        var cpf = new Cpf("12345678901");

        // Act
        var clienteRetornado = await clienteGateway.GetClienteByCpfAsync(cpf);

        // Assert
        Assert.Null(clienteRetornado);
    }

    [Fact]
    public async Task InsertCliente_DeveInserirNovoCliente()
    {
        // Arrange
        var clienteGateway = new ClienteGateway();
        var cpf = new Cpf("12345678901");
        var cliente = new Cliente(cpf, "João Silva", new EmailAddress("teste@gmail.com"));

        // Act
        var clienteInserido = await clienteGateway.InsertCliente(cliente);

        // Assert
        Assert.NotNull(clienteInserido);
        Assert.Equal(cliente.Cpf, clienteInserido.Cpf);
        Assert.Equal(cliente.Nome, clienteInserido.Nome);
    }

    [Fact]
    public async Task ObterClientePorCpfAsync_DeveRetornarClienteQuandoExistir()
    {
        // Arrange
        var mockClienteGateway = new Mock<IClienteGateway>();
        var cpf = new Cpf("12345678901");
        var cliente = new Cliente(cpf, "João Silva", new EmailAddress("teste@gmail.com"));

        mockClienteGateway.Setup(gateway => gateway.GetClienteByCpfAsync(cpf))
            .ReturnsAsync(cliente);

        var clienteService = new ClienteService(mockClienteGateway.Object);

        // Act
        var clienteRetornado = await clienteService.ObterClientePorCpfAsync(cpf);

        // Assert
        Assert.NotNull(clienteRetornado);
        Assert.Equal(cliente.Cpf, clienteRetornado?.Cpf);
    }

    [Fact]
    public async Task ObterClientePorCpfAsync_DeveRetornarNullQuandoCpfNaoExistir()
    {
        // Arrange
        var mockClienteGateway = new Mock<IClienteGateway>();
        var cpf = new Cpf("12345678901");

        mockClienteGateway.Setup(gateway => gateway.GetClienteByCpfAsync(cpf))
            .ReturnsAsync((Cliente?)null);

        var clienteService = new ClienteService(mockClienteGateway.Object);

        // Act
        var clienteRetornado = await clienteService.ObterClientePorCpfAsync(cpf);

        // Assert
        Assert.Null(clienteRetornado);
    }

    [Fact]
    public async Task AdicionarNovoClienteAsync_DeveAdicionarNovoCliente()
    {
        // Arrange
        var mockClienteGateway = new Mock<IClienteGateway>();
        var cpf = new Cpf("12345678901");
        var cliente = new Cliente(cpf, "João Silva", new EmailAddress("teste@gmail.com"));

        mockClienteGateway.Setup(gateway => gateway.InsertCliente(cliente))
            .ReturnsAsync(cliente);

        var clienteService = new ClienteService(mockClienteGateway.Object);

        // Act
        var clienteInserido = await clienteService.AdicionarNovoClienteAsync(cliente);

        // Assert
        Assert.NotNull(clienteInserido);
        Assert.Equal(cliente.Cpf, clienteInserido.Cpf);
        Assert.Equal(cliente.Nome, clienteInserido.Nome);
    }

}


public class ClienteGateway : IClienteGateway
{
    private readonly List<Cliente> _clientes = new List<Cliente>();

    public Task<Cliente?> GetClienteByCpfAsync(Cpf cpf)
    {
        var cliente = _clientes.FirstOrDefault(c => c.Cpf.Equals(cpf));
        return Task.FromResult(cliente);
    }

    public Task<Cliente> InsertCliente(Cliente cliente)
    {
        _clientes.Add(cliente);
        return Task.FromResult(cliente);
    }
}

public class ClienteService
{
    private readonly IClienteGateway _clienteGateway;

    public ClienteService(IClienteGateway clienteGateway)
    {
        _clienteGateway = clienteGateway;
    }

    public async Task<Cliente?> ObterClientePorCpfAsync(Cpf cpf)
    {
        return await _clienteGateway.GetClienteByCpfAsync(cpf);
    }

    public async Task<Cliente> AdicionarNovoClienteAsync(Cliente cliente)
    {
        return await _clienteGateway.InsertCliente(cliente);
    }
}