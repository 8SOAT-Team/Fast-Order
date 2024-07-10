using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Service;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class ClienteExtensions
{
    public static void AddEndpointClientes(this WebApplication app)
    {
        const string EndpointTag = "Clientes";

        app.MapGet("/cliente", async ([FromServices] IMapper mapper, [FromServices] IClienteService service, [FromQuery] string cpf) =>
        {
            var cliente = await service.GetClienteByCpfAsync(cpf);
            var clienteDto = mapper.Map<ClienteDTO>(cliente);
            return Results.Ok(clienteDto);

        }).WithTags(EndpointTag).WithSummary("Encontre um cliente pelo seu CPF").WithOpenApi();

        app.MapPut("/cliente", async ([FromServices] IMapper mapper, [FromServices] IClienteService service, [FromBody] ClienteDTO request) =>
        {
            var cliente = mapper.Map<Cliente>(request);
            cliente = await service.CreateClienteAsync(cliente);

            var clienteDto = mapper.Map<ClienteDTO>(cliente);

            return Results.Ok(clienteDto);
        }).WithTags(EndpointTag).WithSummary("Encontre um cliente pelo seu CPF").WithOpenApi();
    }
}