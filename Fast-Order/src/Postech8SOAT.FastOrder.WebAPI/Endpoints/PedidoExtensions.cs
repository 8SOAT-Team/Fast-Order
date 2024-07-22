using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Service;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class PedidoExtensions
{
    public static void AddEndpointPedido(this WebApplication app)
    {
        const string PedidoTag = "Pedido";

        app.MapPost("/pedido", async ([FromServices] IMapper mapper, [FromServices] IProdutoService service) =>
            {

            }).WithTags(PedidoTag).WithSummary("Crie um pedido informando os itens.").WithOpenApi();

        app.MapGet("/pedido/{id:guid}", async ([FromServices] IMapper mapper, [FromServices] IProdutoService service, [FromRoute] Guid id) =>
            {

            }).WithTags(PedidoTag).WithSummary("Obtenha um pedido").WithOpenApi();

        app.MapGet("/pedido", async ([FromServices] IMapper mapper, [FromServices] IProdutoService service) =>
            {

            }).WithTags(PedidoTag).WithSummary("Liste pedidos").WithOpenApi();
    }
}