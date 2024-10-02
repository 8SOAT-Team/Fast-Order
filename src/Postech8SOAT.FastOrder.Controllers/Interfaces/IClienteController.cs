using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Types.Results;

namespace Postech8SOAT.FastOrder.Controllers.Interfaces;
public interface IClienteController
{
    Task<Result<ClienteIdentificadoDto>> IdentificarClienteAsync(string document);
    Task<Result<ClienteIdentificadoDto>> CriarNovoClienteAsync(NovoClienteDto newCliente);
}
