using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Integration.Tests.Builder;
using Postech8SOAT.FastOrder.Integration.Tests.HostTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Postech8SOAT.FastOrder.Integration.Tests;
public class FastOrder_ClienteTest : IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;

    public FastOrder_ClienteTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GET_Deve_buscar_cliente_CPF()
    {
        //Arrange
        var clienteExistente = _factory.Context.Clientes.FirstOrDefault();
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

}
