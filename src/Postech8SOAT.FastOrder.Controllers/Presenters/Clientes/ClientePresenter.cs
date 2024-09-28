using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Presenters.Clientes;

public static class ClientePresenter
{
    public static ClienteIdentificadoDto AdaptClienteIdentificado(Cliente cliente) => new(cliente.Id, cliente.Nome);

    public static ClienteDTO AdaptCliente(Cliente cliente) => new()
    {
        Id = cliente.Id,
        Nome = cliente.Nome,
        Email = cliente.Email,
        Cpf = cliente.Cpf,
    };
}

