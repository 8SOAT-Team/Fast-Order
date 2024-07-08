using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository.Base;

namespace Postech8SOAT.FastOrder.Domain.Ports.Repository;
public interface IClienteRepository : IRepository<Cliente>
{
     Task<Cliente> GetClienteByCpfAsync(string cpf);
     Task<Cliente> GetClienteByEmailAsync(string email);
}
