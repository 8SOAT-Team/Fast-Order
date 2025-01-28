using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Integration.Tests.Builder;
using Postech8SOAT.FastOrder.Integration.Tests.HostTest;
using Postech8SOAT.FastOrder.WebAPI.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace Postech8SOAT.FastOrder.Integration.Tests;
public class FastOrder_PagamentoExtensionsTest: IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;
    private readonly HttpClient _client;
    public FastOrder_PagamentoExtensionsTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    //[Fact]
    //public async Task Post_IniciarPagamento_ReturnsCreated()
    //{
    //    // Arrange
    //    var cliente = _factory.Context.Clientes.FirstOrDefault();

    //    if (cliente is null)
    //    {
    //        cliente = new ClienteBuilder().Build();
    //        _factory.Context.Clientes.Add(cliente);
    //        _factory.Context.SaveChanges();
    //    }

        
    //    var pedidoExistente = _factory.Context.Pedidos.FirstOrDefault();
    //    if (pedidoExistente is null)
    //    {

    //         pedidoExistente = new PedidoBuilder(cliente.Id).Build();
    //        _factory.Context.Pedidos.Add(pedidoExistente);
    //        _factory.Context.SaveChanges();
    //    }
    //    var pedidoId = pedidoExistente!.Id;
    //    var request = new NovoPagamentoDTO { MetodoDePagamento = MetodosDePagamento.Master };

    //    // Act
    //    var response = await _client.PostAsJsonAsync($"/pagamento/pedido/{pedidoId}", request);

    //    // Assert
    //    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    //}

    [Fact]
    public async Task Patch_ConfirmarPagamento_ReturnsOk()
    {
        // Arrange
        var cliente = _factory.Context.Clientes.FirstOrDefault();

        if (cliente is null)
        {
            cliente = new ClienteBuilder().Build();
            _factory.Context.Clientes.Add(cliente);
            _factory.Context.SaveChanges();
        }

        var pedidoExistente = _factory.Context.Pedidos.FirstOrDefault();
        if (pedidoExistente is null)
        {
            pedidoExistente = new PedidoBuilder(cliente.Id).Build();
            _factory.Context.Pedidos.Add(pedidoExistente);
            _factory.Context.SaveChanges();
        }

        var pagamentoExistente = _factory.Context.Pagamentos.FirstOrDefault();
        if (pagamentoExistente is null)
        {
    
            var pedido = new PedidoBuilder(cliente.Id).Build();
            pagamentoExistente = new PagamentoBuilder().ComPedido(pedidoExistente).Build();
            ;
            _factory.Context.Pagamentos.Add(pagamentoExistente);
            _factory.Context.SaveChanges();
        }
        var pagamentoId = pagamentoExistente.Id;
        var request = new ConfirmarPagamentoDTO(StatusDoPagamento.Autorizado);

        // Act
        var response = await _client.PatchAsJsonAsync($"/pagamento/{pagamentoId}", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_GetPagamentoByPedido_ReturnsOk()
    {
        // Arrange
        var cliente = _factory.Context.Clientes.FirstOrDefault();

        if (cliente is null)
        {
            cliente = new ClienteBuilder().Build();
            _factory.Context.Clientes.Add(cliente);
            _factory.Context.SaveChanges();
        }

        var pedidoExistente = _factory.Context.Pedidos.FirstOrDefault();
        if (pedidoExistente is null)
        {       
             pedidoExistente = new PedidoBuilder(cliente.Id).Build();
            _factory.Context.Pedidos.Add(pedidoExistente);
            _factory.Context.SaveChanges();
        }
        var pedidoId = pedidoExistente!.Id;

        var pagamentoExistente = _factory.Context.Pagamentos.FirstOrDefault();
        if (pagamentoExistente is null)
        {

            var pedido = new PedidoBuilder(cliente.Id).Build();
            var pagamento = new PagamentoBuilder().ComPedido(pedidoExistente).Build();
            _factory.Context.Pagamentos.Add(pagamento);
            _factory.Context.SaveChanges();
        }

        // Act
        var response = await _client.GetAsync($"/pagamento/pedido/{pedidoId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


}
