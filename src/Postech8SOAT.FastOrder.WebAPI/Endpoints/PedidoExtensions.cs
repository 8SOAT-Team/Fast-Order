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

        app.MapPost("/pedido", async ([FromServices] IPedidoController pedidoController,
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
        .Produces<PedidoDTO>((int)HttpStatusCode.Created)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithSummary("Crie um pedido informando os itens.")
        .WithOpenApi();

        app.MapGet("/pedido/{id:guid}", async ([FromServices] IPedidoController pedidoController, [FromRoute] Guid id) =>
        {
            var pedido = await pedidoController.GetPedidoByIdAsync(id);
            return pedido.GetResult();
        }).WithTags(PedidoTag)
        .Produces<PedidoDTO>((int)HttpStatusCode.Created)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithSummary("Obtenha um pedido")
        .WithOpenApi();

        app.MapGet("/pedido", async ([FromServices] IPedidoController pedidoController) =>
        {
            var result = await pedidoController.GetAllPedidosAsync();
            return result.GetResult();
        }).WithTags(PedidoTag)
        .Produces<List<PedidoDTO>>((int)HttpStatusCode.Created)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithSummary("Liste pedidos")
        .WithOpenApi();

        app.MapGet("/pedido/status", async ([FromServices] IPedidoController pedidoController) =>
        {
            var pedidos = await pedidoController.GetAllPedidosPending();
            return pedidos.GetResult();
        }).WithTags(PedidoTag)
        .Produces<List<PedidoDTO>>((int)HttpStatusCode.Created)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithSummary("Lista de pedidos Pendentes (Pronto > Em Preparação > Recebido)")
        .WithOpenApi();

        app.MapPut("/pedido/{id:guid}/status", async ([FromServices] IPedidoController pedidoController,
        [FromRoute] Guid id,
        [FromBody] AtualizarStatusDoPedidoDTO request,
        HttpContext httpContext) =>
        {
            var result = await pedidoController.AtualizarStatusDePreparacaoDoPedido(request.NovoStatus, id);
            return result.GetResult();
        }).WithTags(PedidoTag)
        .Produces<PedidoDTO>((int)HttpStatusCode.Created)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithSummary("Atualize o status de um pedido")
        .WithOpenApi();
    }
}
