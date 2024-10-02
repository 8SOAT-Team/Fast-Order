using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository.Base;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;
public interface IClienteRepository : IRepository<Cliente>
{
    Task<Cliente?> GetClienteByCpfAsync(Cpf cpf);
    Task<Cliente?> GetClienteByEmailAsync(string email);
}
