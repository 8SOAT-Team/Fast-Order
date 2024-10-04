using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.WebAPI.DTOs;
using Postech8SOAT.FastOrder.WebAPI.Endpoints.Extensions;
using System.Net;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class PedidoExtensions
{
    public static void AddEndpointPedidos(this WebApplication app)
    {
        const string PedidoTag = "Pedido";

        app.MapPost("/pedido", async ([FromServices] IMapper mapper,
            [FromServices] IPedidoController pedidoController,
            [FromBody] NovoPedidoDTO request,
            HttpContext httpContext) =>
            {
                var pedidoCriado = await pedidoController.CreatePedidoAsync(request);

                IResult result = null!;

                pedidoCriado.Match(
                    onSuccess: (p) => result = Results.Created($"/pedido/{p.Id}", p),
                    onFailure: (errors) => result = pedidoCriado.GetFailureResult());

                return result;

            }).WithTags(PedidoTag)
            .Produces<PedidoCriadoDTO>((int)HttpStatusCode.Created)
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithSummary("Crie um pedido informando os itens.")
            .WithOpenApi();

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

        app.MapGet("/pedido/status", async ([FromServices] IPedidoController pedidoController) =>
        {
            var pedidos = await pedidoController.GetAllPedidosShowStatusAsync();
            return pedidos.GetResult();
        }).WithTags(PedidoTag).WithSummary("Lista de pedidos baseado no status").WithOpenApi();

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