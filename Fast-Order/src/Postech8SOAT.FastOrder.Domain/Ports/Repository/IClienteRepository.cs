using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository.Base;
using Postech8SOAT.FastOrder.Domain.ValueObjects;

namespace Postech8SOAT.FastOrder.Domain.Ports.Repository;
public interface IClienteRepository : IRepository<Cliente>
{
     Task<Cliente?> GetClienteByCpfAsync(Cpf cpf);
     Task<Cliente?> GetClienteByEmailAsync(string email);
}
