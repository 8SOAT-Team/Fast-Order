using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Upstream.Pagamentos.MercadoPago.Dtos;
using Postech8SOAT.FastOrder.WebAPI.Endpoints.Extensions;
using System.Net;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class PagamentoWebhookExtensions
{
    public static void AddEndpointWebhook(this WebApplication app)
    {
        app.MapPost("/pagamento/mp/webhook", async ([FromQuery] string type,
            [FromBody] PagamentoWebhookDTO payload,
            [FromServices] IPagamentoController controller,
             HttpContext context) =>
        {
            var dataId = context.Request.Query["data.id"].ToString();

            if(type != "payment")
            {
                return Results.Ok();
            }

            var result = await controller.ReceberWebhookPagamento(dataId);

            return result.GetResult();
        }).WithTags("Webhook")
        .Produces((int)HttpStatusCode.OK)
        .WithOpenApi(operation => new(operation)
        {
            Parameters =
            [
                new OpenApiParameter
                {
                    Name = "data.id",
                    In = ParameterLocation.Query,
                    Required = true,
                    Description = "O ID do pagamento.",
                    Schema = new OpenApiSchema
                    {
                        Type = "string"
                    }
                },
                new OpenApiParameter
                {
                    Name = "type",
                    In = ParameterLocation.Query,
                    Required = true,
                    Description = "tipo do evento",
                    Schema = new OpenApiSchema
                    {
                        Type = "string"
                    }
                },
            ]
        });
    }
}