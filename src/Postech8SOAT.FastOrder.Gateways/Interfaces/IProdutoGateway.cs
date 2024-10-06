using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Gateways.Interfaces;
public interface IProdutoGateway
{
    Task<Produto> CreateProdutoAsync(Produto produto);
    Task<Produto?> GetProdutoByIdAsync(Guid id);
    Task<Produto?> GetProdutoCompletoByIdAsync(Guid id);
    Task<ICollection<Produto>> GetProdutosByCategoriaAsync(Guid categoriaId);
    Task<ICollection<Produto>> ListarTodosProdutosAsync();
    Task<ICollection<Produto>> ListarProdutosByIdAsync(ICollection<Guid> ids);
}