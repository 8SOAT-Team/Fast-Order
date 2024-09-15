using Postech8SOAT.FastOrder.Application.Adapters.Presenters.Clientes.Models;
using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Application.Adapters.Presenters.Clientes;

public static class ClientePresenter
{
    public static ClienteIdentificadoModel AdaptClienteIdentificado(Cliente cliente) => new(cliente.Id, cliente.Nome);
}
