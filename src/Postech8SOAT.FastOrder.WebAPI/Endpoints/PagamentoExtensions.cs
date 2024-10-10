using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Dtos;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.WebAPI.DTOs;
using Postech8SOAT.FastOrder.WebAPI.Endpoints.Extensions;
using System.Net;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class PagamentoExtensions
{
    private const string PagamentoTag = "Pagamentos";
    public static void AddEndpointPagamentos(this WebApplication app)
    {
        app.MapPost("/pagamento/pedido/{pedidoId:guid}", async (
            [FromServices] IPagamentoController pagamentoController,
            [FromRoute] Guid pedidoId,
            [FromBody] NovoPagamentoDTO request) =>
        {
            var useCaseResult = await pagamentoController.IniciarPagamento(pedidoId, request.MetodoDePagamento);
            return useCaseResult.GetResult();
        }).WithTags(PagamentoTag)
        .WithSummary("Inicialize um pagamento de um pedido.")
        .Produces<PagamentoResponseDTO>((int)HttpStatusCode.Created)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();

        app.MapPatch("/pagamento/{pagamentoId:guid}", async ([FromServices] IPagamentoController pagamentoController,
           [FromRoute] Guid pagamentoId,
           [FromBody] ConfirmarPagamentoDTO request,
           HttpContext httpContext) =>
        {
            await pagamentoController.ConfirmarPagamento(pagamentoId, request.Status);
            return Results.Ok();
        }).WithTags(PagamentoTag).WithSummary("Confirma o pagamento de um pedido.").WithOpenApi();


        app.MapGet("/pagamento/pedido/{pedidoId:guid}", async ([FromServices] IPagamentoController pagamentoController,
            [FromRoute] Guid pedidoId) =>
        {
            var useCaseResult = await pagamentoController.GetPagamentoByPedidoAsync(pedidoId);
            return useCaseResult.GetResult();
        }).WithTags(PagamentoTag)
        .WithSummary("Obtenha os dados de um pagamento pelo id do pedido.")
        .Produces<PagamentoResponseDTO>((int)HttpStatusCode.OK)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();

        app.MapPost("/pagamento/webhook", async ([FromBody] object payload) =>
        {
            await Console.Out.WriteLineAsync("Funcionou!!!");
            return Results.Ok(payload);
        });
    }
}
