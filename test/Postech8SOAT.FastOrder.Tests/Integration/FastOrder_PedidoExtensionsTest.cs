﻿using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Tests.Integration.Builder;
using Postech8SOAT.FastOrder.Tests.Integration.HostTest;
using System.Net;
using System.Net.Http.Json;

namespace Postech8SOAT.FastOrder.Tests.Integration;
public class FastOrder_PedidoExtensionsTest : IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;

    public FastOrder_PedidoExtensionsTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task POST_Deve_criar_pedido()
    {
        //Arrange
        var cliente = _factory.Context.Clientes.FirstOrDefault();

        if (cliente is null)
        {
            cliente = new ClienteBuilder().Build();
            _factory.Context.Clientes.Add(cliente);
            _factory.Context.SaveChanges();
        }

        var pedidoDto = new NovoPedidoDTOBuilder(cliente.Id).Build();


        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/pedido", pedidoDto);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task POST_Nao_Deve_Criar_Pedido_Com_Dados_Invalidos()
    {
        //Arrange
        var pedidoDto = new NovoPedidoDTOInvalidoBuilder().Build();
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/pedido", pedidoDto);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GET_Deve_Retornar_Pedido_Pelo_Id()
    {
        //Arrange
        var cliente = _factory.Context!.Clientes.FirstOrDefault();

        if (cliente is null)
        {
            cliente = new ClienteBuilder().Build();
            _factory.Context.Clientes.Add(cliente);
            _factory.Context.SaveChanges();
        }

        var pedidoExistente = _factory.Context!.Pedidos.FirstOrDefault();
        if (pedidoExistente is null)
        {
            pedidoExistente = new PedidoBuilder(cliente.Id).Build();
            _factory.Context.Add(pedidoExistente);
            _factory.Context.SaveChanges();
        }
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.GetFromJsonAsync<NovoPedidoDTO>($"/pedido/{pedidoExistente.Id}");
        //Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task GET_Deve_Retornar_Todos_Pedidos()
    {
        //Arrange
        var pedidoExistente = _factory.Context!.Pedidos.FirstOrDefault();
        if (pedidoExistente is null)
        {
            var cliente = _factory.Context.Clientes.FirstOrDefault();

            if (cliente is null)
            {
                cliente = new ClienteBuilder().Build();
                _factory.Context.Clientes.Add(cliente);
                _factory.Context.SaveChanges();
            }
            pedidoExistente = new PedidoBuilder(cliente.Id).Build();
            _factory.Context.Add(pedidoExistente);
            _factory.Context.SaveChanges();
        }


        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.GetFromJsonAsync<ICollection<NovoPedidoDTO>>($"/pedido");
        //Assert
        Assert.NotNull(response);
    }

    //[Fact]
    //public async Task PUT_Deve_Atualizar_Status_Pedido()
    //{
    //    //Arrange
    //    var pedidoExistente = _factory.Context!.Pedidos.FirstOrDefault();
    //    if (pedidoExistente is null)
    //    {
    //        var cliente = _factory.Context.Clientes.FirstOrDefault();

    //        if (cliente is null)
    //        {
    //            cliente = new ClienteBuilder().Build();
    //            _factory.Context.Clientes.Add(cliente);
    //            _factory.Context.SaveChanges();
    //        }
    //        pedidoExistente = new PedidoBuilder(cliente.Id).Build();
    //        _factory.Context.Add(pedidoExistente);
    //        _factory.Context.SaveChanges();
    //    }

    //    var atualizaStatusPedidoExistenteDTO = new AtualizarStatusDoPedidoDTOBuilder().Build();

    //    var httpClient = _factory.CreateClient();
    //    //Act       
    //    var response = await httpClient.PutAsJsonAsync($"/pedido/{pedidoExistente.Id}/status", atualizaStatusPedidoExistenteDTO);
    //    //Assert
    //    Assert.NotNull(response);
    //    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    //}
}

