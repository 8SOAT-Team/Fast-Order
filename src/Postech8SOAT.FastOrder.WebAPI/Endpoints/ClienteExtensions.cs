﻿using AutoMapper;
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

        }).WithTags(EndpointTag).WithSummary("Identifique um cliente pelo seu CPF").WithOpenApi();

        app.MapGet("/cliente/{id:guid}", async ([FromServices] IMapper mapper, [FromServices] IClienteService service, [FromRoute] Guid id) =>
        {
            var cliente = await service.GetClienteByIdAsync(id);
            var clienteDto = mapper.Map<ClienteDTO>(cliente);
            return Results.Ok(clienteDto);

        }).WithTags(EndpointTag).WithSummary("Identifique um cliente pelo seu Identificador").WithOpenApi();

        app.MapPost("/cliente", async ([FromServices] IMapper mapper, [FromServices] IClienteService service, [FromBody] ClienteDTO request) =>
        {
            var cliente = mapper.Map<Cliente>(request);
            cliente = await service.CreateClienteAsync(cliente);

            var clienteDto = mapper.Map<ClienteDTO>(cliente);

            return Results.Ok(clienteDto);
        }).WithTags(EndpointTag).WithSummary("Cadastre um novo cliente").WithOpenApi();    
    }
}