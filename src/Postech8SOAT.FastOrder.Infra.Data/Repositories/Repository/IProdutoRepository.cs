using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository.Base;

namespace Postech8SOAT.FastOrder.Infra.Data.Repositories.Repository;
public interface IProdutoRepository : IRepository<Produto>
{
    Task<ICollection<Produto>> GetProdutosByCategoriaAsync(Guid categoriaId);
    Task<Categoria?> GetCategoriaByIdAsync(Guid categoriaId);
    Task<List<Categoria>> FindAllCategoriasAsync();
    Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId);
}
