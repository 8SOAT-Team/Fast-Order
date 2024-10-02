using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.WebAPI.Endpoints.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class ClienteExtensions
{
    public static void AddEndpointClientes(this WebApplication app)
    {
        const string EndpointTag = "Clientes";

        app.MapGet("/cliente", async ([FromServices] IClienteController controller, [FromQuery, Required] string cpf) =>
        {
            var useCaseResult = await controller.IdentificarClienteAsync(cpf);
            return useCaseResult.GetResult();
        }).WithTags(EndpointTag).WithSummary("Identifique um cliente pelo seu CPF")
        .Produces<ClienteIdentificadoDto>((int)HttpStatusCode.OK)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();

        app.MapPost("/cliente", async ([FromServices] IClienteController controller, [FromBody] NovoClienteDto request) =>
        {
            var useCaseResult = await controller.CriarNovoClienteAsync(request);

            return useCaseResult.GetResult();
        }).WithTags(EndpointTag).WithSummary("Cadastre um novo cliente")
        .Produces<ClienteIdentificadoDto>((int)HttpStatusCode.OK)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .WithOpenApi();
    }
}