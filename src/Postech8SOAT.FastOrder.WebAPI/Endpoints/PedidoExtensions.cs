using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Application.Commands.Pedidos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Service;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class PedidoExtensions
{
    public static void AddEndpointPedidos(this WebApplication app)
    {
        const string PedidoTag = "Pedido";

        app.MapPost("/pedido", async ([FromServices] IMapper mapper,
            [FromServices] IPedidoService service,
            [FromServices] IProdutoService prodService,
            [FromBody] NovoPedidoDTO request,
            HttpContext httpContext) =>
            {
                var pedido = mapper.Map<Pedido>(request);


                foreach (var item in pedido.ItensDoPedido)
                {
                    item.Produto = prodService.GetProdutoByIdAsync(item.ProdutoId).Result!;
                }

                pedido.CalcularValorTotal();

                var pedidoCriado = await service.CreatePedidoAsync(pedido);
                var pedidoResposta = mapper.Map<PedidoResponseDTO>(pedidoCriado);
                return Results.Created($"/pedido/{pedidoCriado.Id}", pedidoResposta);
            }).WithTags(PedidoTag).WithSummary("Crie um pedido informando os itens.").WithOpenApi();

        app.MapGet("/pedido/{id:guid}", async ([FromServices] IMapper mapper, [FromServices] IPedidoService service, [FromRoute] Guid id) =>
        {
            var pedido = await service.GetPedidoByIdAsync(id);
            var pedidoResposta = mapper.Map<NovoPedidoDTO>(pedido);

            return Results.Ok(pedidoResposta);
        }).WithTags(PedidoTag).WithName("ObterPedidoPorId").WithSummary("Obtenha um pedido").WithOpenApi();

        app.MapGet("/pedido", async ([FromServices] IMapper mapper, [FromServices] IPedidoService service) =>
            {
                var pedidos = await service.GetAllPedidosAsync();
                var listaDePedidos = pedidos.Select(p => mapper.Map<NovoPedidoDTO>(p));

                return Results.Ok(listaDePedidos);
            }).WithTags(PedidoTag).WithSummary("Liste pedidos").WithOpenApi();

        app.MapPut("/pedido/{id:guid}/status", async ([FromServices] IMapper mapper,
            [FromServices] IPedidoService service,
            [FromServices] IPedidoServiceCommandInvoker commandInvoker,
            [FromRoute] Guid id,
            [FromBody] AtualizarStatusDoPedidoDTO request,
            HttpContext httpContext) =>
            {
                await commandInvoker.ExecutarComandoAsync(request.NovoStatus, id, service);
                return Results.Accepted($"/pedido/{id}");
            }).WithTags(PedidoTag).WithSummary("Atualize o status de um pedido").WithOpenApi();
    }
}