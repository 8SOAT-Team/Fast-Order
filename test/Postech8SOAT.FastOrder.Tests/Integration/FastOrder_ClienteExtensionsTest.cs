using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Tests.Integration.Builder;
using Postech8SOAT.FastOrder.Tests.Integration.HostTest;
using System.Net;
using System.Net.Http.Json;

namespace Postech8SOAT.FastOrder.Tests.Integration;
public class FastOrderClienteExtensionsTest : IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;

    public FastOrderClienteExtensionsTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GET_Deve_buscar_cliente_CPF()
    {
        //Arrange
        var clienteExistente = _factory.Context!.Clientes.FirstOrDefault();
        if (clienteExistente is null)
        {
            clienteExistente = new ClienteBuilder().Build();

            _factory.Context.Add(clienteExistente);
            _factory.Context.SaveChanges();
        }

        var httpClient = _factory.CreateClient();

        //Act
        var response = await httpClient.GetFromJsonAsync<ClienteIdentificadoDto>($"/cliente?cpf=" + clienteExistente.Cpf);

        //Assert
        Assert.NotNull(response);

    }
    [Fact]
    public async Task GET_Nao_Deve_buscar_Cliente_CPF_Invalido()
    {
        //Arrange
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.GetAsync($"/cliente?cpf=12345678901");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]

    public async Task POST_Deve_criar_cliente()
    {
        //Arrange
        var clienteDto = new NovoClienteDTOBuilder().Build();

        var httpClient = _factory.CreateClient();

        //Act
        var response = await httpClient.PostAsJsonAsync("/cliente", clienteDto);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }

    [Fact]
    public async Task POST_Nao_Deve_Criar_Cliente_Invalido()
    {
        //Arrange
        var clienteDto = new NovoClienteDTOBuilderInvalido().Build();
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/cliente", clienteDto);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

}
