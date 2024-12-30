using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Integration.Tests.HostTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Postech8SOAT.FastOrder.Integration.Tests;
public class FastOrder_ClienteTest:IClassFixture<FastOrderWebApplicationFactory>
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
            clienteExistente = new Cliente("185.681.710-52", "Bill Gates", "bill@email.com");

            _factory.Context.Add(clienteExistente);
            _factory.Context.SaveChanges();
        }

        var httpClient = _factory.CreateClient();

        //Act
        var response = await httpClient.GetFromJsonAsync<ClienteIdentificadoDto>($"/cliente/"+ clienteExistente.Cpf);

        //Assert
        Assert.NotNull(response);

    }
}
