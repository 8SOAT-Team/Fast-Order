using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Commands.Pedidos;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class PedidoExtensions
{
    public static void AddEndpointPedidos(this WebApplication app)
    {
        const string PedidoTag = "Pedido";

        app.MapPost("/pedido", async ([FromServices] IMapper mapper,
            [FromServices] IPedidoController pedidoController,
            [FromServices] IProdutoUseCase prodService,
            [FromBody] NovoPedidoDTO request,
            HttpContext httpContext) =>
            {
                var pedido = mapper.Map<Pedido>(request);


                foreach (var item in pedido.ItensDoPedido)
                {
                    item.Produto = prodService.GetProdutoByIdAsync(item.ProdutoId).Result!;
                }

                pedido.CalcularValorTotal();

                var pedidoCriado = await pedidoController.CreatePedidoAsync(pedido);
                var pedidoResposta = mapper.Map<PedidoResponseDTO>(pedidoCriado);
                return Results.Created($"/pedido/{pedidoCriado.Id}", pedidoResposta);
            }).WithTags(PedidoTag).WithSummary("Crie um pedido informando os itens.").WithOpenApi();

        app.MapGet("/pedido/{id:guid}", async ([FromServices] IMapper mapper, [FromServices] IPedidoController pedidoController, [FromRoute] Guid id) =>
        {
            var pedido = await pedidoController.GetPedidoByIdAsync(id);
            var pedidoResposta = mapper.Map<NovoPedidoDTO>(pedido);

            return Results.Ok(pedidoResposta);
        }).WithTags(PedidoTag).WithName("ObterPedidoPorId").WithSummary("Obtenha um pedido").WithOpenApi();

        app.MapGet("/pedido", async ([FromServices] IMapper mapper, [FromServices] IPedidoController pedidoController) =>
            {
                var pedidos = await pedidoController.GetAllPedidosAsync();
                var listaDePedidos = pedidos.Select(p => mapper.Map<NovoPedidoDTO>(p));

                return Results.Ok(listaDePedidos);
            }).WithTags(PedidoTag).WithSummary("Liste pedidos").WithOpenApi();

        app.MapPut("/pedido/{id:guid}/status", async ([FromServices] IMapper mapper,
            [FromServices] IPedidoController pedidoController,        
            [FromRoute] Guid id,
            [FromBody] AtualizarStatusDoPedidoDTO request,
            HttpContext httpContext) =>
            {
                await pedidoController.AtualizaStatus(request.NovoStatus, id);
                return Results.Accepted($"/pedido/{id}");
            }).WithTags(PedidoTag).WithSummary("Atualize o status de um pedido").WithOpenApi();
    }
}