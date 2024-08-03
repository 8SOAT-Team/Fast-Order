using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Domain.Ports.Service;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class PagamentoExtensions
{
    private const string PagamentoTag = "Pagamentos";
    public static void AddEndpointPagamentos(this WebApplication app)
    {
        app.MapPost("/pagamento/pedido/{pedidoId:guid}", async ([FromServices] IMapper mapper,
            [FromServices] IPagamentoService service,
            [FromServices] IPedidoService pedidoService,
            [FromRoute] Guid pedidoId,
            [FromBody] MetodoDePagamento metodoDePagamento,
            HttpContext httpContext) =>
        {
            var pedido = await pedidoService.GetPedidoByIdAsync(pedidoId);

            if(pedido is null)
            {
                return Results.NotFound($"Pedido {pedidoId} não localizado.");
            }

            var pagamento = await service.CreatePagamentoAsync(pedido, metodoDePagamento);
            return Results.Ok(pagamento);
        }).WithTags(PagamentoTag).WithSummary("Inicialize um pagamento de um pedido.").WithOpenApi();

        app.MapPatch("/pagamento/{pagamentoId:guid}", async ([FromServices] IMapper mapper,
           [FromServices] IPagamentoService service,
           [FromRoute] Guid pagamentoId,
           [FromBody] StatusPagamento status,
           HttpContext httpContext) =>
        {
            await service.ConfirmarPagamento(pagamentoId, status);
            return Results.Ok();
        }).WithTags(PagamentoTag).WithSummary("Confirma o pagamento de um pedido.").WithOpenApi();


        app.MapGet("/pagamento/pedido/{pedidoId:guid}", async ([FromServices] IMapper mapper,
            [FromServices] IPagamentoService service,
            [FromRoute] Guid pedidoId,
            HttpContext httpContext) =>
        {
            var pagamento = await service.GetPagamentoByPedidoAsync(pedidoId);
            return Results.Ok(pagamento);
        }).WithTags(PagamentoTag).WithSummary("Obtenha os dados de um pagamento pelo id do pedido.").WithOpenApi();

        app.MapGet("/pagamento/{pagamentoId:guid}", async ([FromServices] IMapper mapper,
            [FromServices] IPagamentoService service,
            [FromRoute] Guid pagamentoId,
            HttpContext httpContext) =>
        {
            var pagamento = await service.GetPagamentoAsync(pagamentoId);
            return Results.Ok(pagamento);
        }).WithTags(PagamentoTag).WithSummary("Obtenha os dados de um pagamento pelo id do pedido.").WithOpenApi();

        app.MapGet("/pagamento", async ([FromServices] IMapper mapper,
            [FromServices] IPagamentoService service,
            [FromServices] IPedidoService pedidoService,
            HttpContext httpContext) =>
        {
            var pagamento = await service.ListPagamentos();
            return Results.Ok(pagamento);
        }).WithTags(PagamentoTag).WithSummary("Liste os pagamentos já criados.").WithOpenApi();
    }
}
