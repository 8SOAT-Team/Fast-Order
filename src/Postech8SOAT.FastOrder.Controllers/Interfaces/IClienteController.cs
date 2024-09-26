using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Types.Results;

namespace Postech8SOAT.FastOrder.Controllers.Interfaces;
public interface IClienteController
{
    Task<Cliente> CreateClienteAsync(Cliente cliente);

    Task<Cliente> DeleteClienteAsync(Cliente cliente);

    Task<List<Cliente>> GetAllClientesAsync();

    Task<Cliente?> GetClienteByCpfAsync(string cpf);

    Task<Cliente?> GetClienteByEmailAsync(string email);

    Task<Cliente> GetClienteByIdAsync(Guid id);

    Task<Cliente> UpdateClienteAsync(Cliente cliente);

    Task<Result<ClienteIdentificadoDto>> IdentificarClienteAsync(string document);
    Task<Result<ClienteIdentificadoDto>> CriarNovoClienteAsync(NovoClienteDto newCliente);
}
