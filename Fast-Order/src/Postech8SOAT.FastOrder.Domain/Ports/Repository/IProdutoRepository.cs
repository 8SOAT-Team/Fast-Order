using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository.Base;

namespace Postech8SOAT.FastOrder.Domain.Ports.Repository;
public interface IProdutoRepository : IRepository<Produto>
{
    Task<ICollection<Produto>> GetProdutosByCategoria(Guid categoriaId);
}
