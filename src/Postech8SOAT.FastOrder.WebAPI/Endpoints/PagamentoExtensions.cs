using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class PagamentoExtensions
{
    private const string PagamentoTag = "Pagamentos";
    public static void AddEndpointPagamentos(this WebApplication app)
    {
        app.MapPost("/pagamento/pedido/{pedidoId:guid}", async ([FromServices] IMapper mapper,
            [FromServices] IPagamentoController pagamentoController,
            [FromServices] IPedidoController pedidoController,
            [FromRoute] Guid pedidoId,
            [FromBody] NovoPagamentoDTO request,
            HttpContext httpContext) =>
        {
            var pedido = await pedidoController.GetPedidoByIdAsync(pedidoId);

            if (pedido is null)
            {
                return Results.NotFound($"Pedido {pedidoId} não localizado.");
            }

            var pagamento = await pagamentoController.CreatePagamentoAsync(pedido, request.MetodoDePagamento);

            var pagamentoResponse = mapper.Map<PagamentoDTO>(pagamento);

            return Results.Ok(pagamentoResponse);

        }).WithTags(PagamentoTag).WithSummary("Inicialize um pagamento de um pedido.").WithOpenApi();

        app.MapPatch("/pagamento/{pagamentoId:guid}", async ([FromServices] IMapper mapper,
           [FromServices] IPagamentoController pagamentoController,
           [FromRoute] Guid pagamentoId,
           [FromBody] ConfirmarPagamentoDTO request,
           HttpContext httpContext) =>
        {
            await pagamentoController.ConfirmarPagamento(pagamentoId, request.Status);
            return Results.Ok();
        }).WithTags(PagamentoTag).WithSummary("Confirma o pagamento de um pedido.").WithOpenApi();


        app.MapGet("/pagamento/pedido/{pedidoId:guid}", async ([FromServices] IMapper mapper,
            [FromServices] IPagamentoController pagamentoController,
            [FromRoute] Guid pedidoId,
            HttpContext httpContext) =>
        {
            var pagamento = await pagamentoController.GetPagamentoByPedidoAsync(pedidoId);
            var pagamentoResponse = mapper.Map<PagamentoDTO>(pagamento);
            return Results.Ok(pagamentoResponse);
        }).WithTags(PagamentoTag).WithSummary("Obtenha os dados de um pagamento pelo id do pedido.").WithOpenApi();

        app.MapGet("/pagamento/{pagamentoId:guid}", async ([FromServices] IMapper mapper,
            [FromServices] IPagamentoController pagamentoController,
            [FromRoute] Guid pagamentoId,
            HttpContext httpContext) =>
        {
            var pagamento = await pagamentoController.GetPagamentoAsync(pagamentoId);
            var pagamentoResponse = mapper.Map<PagamentoDTO>(pagamento);
            return Results.Ok(pagamentoResponse);
        }).WithTags(PagamentoTag).WithSummary("Obtenha os dados de um pagamento pelo id do pagamento.").WithOpenApi();

        app.MapGet("/pagamento", async ([FromServices] IMapper mapper,
            [FromServices] IPagamentoController pagamentoController,
            [FromServices] IPedidoController pedidoController,
            HttpContext httpContext) =>
        {
            var pagamento = await pagamentoController.ListPagamentos();
            var pagamentoResponse = mapper.Map<List<PagamentoDTO>>(pagamento);
            return Results.Ok(pagamentoResponse);
        }).WithTags(PagamentoTag).WithSummary("Liste os pagamentos já criados.").WithOpenApi();
    }
}
