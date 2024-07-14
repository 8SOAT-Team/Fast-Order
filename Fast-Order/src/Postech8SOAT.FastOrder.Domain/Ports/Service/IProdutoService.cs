using Postech8SOAT.FastOrder.Domain.Entities;
namespace Postech8SOAT.FastOrder.Domain.Ports.Service;
public interface IProdutoService
{
    Task<Produto?> GetProdutoByIdAsync(Guid id);
    Task<Produto?> GetProdutoByNomeAsync(string nome);
    Task<Produto> CreateProdutoAsync(Produto produto);
    Task<Produto> UpdateProdutoAsync(Produto produto);
    Task DeleteProdutoAsync(Produto produto);
    Task<ICollection<Produto>> GetAllProdutosAsync();
    Task<ICollection<Produto>> GetProdutosByCategoria(Guid categoriaId);
    Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId);
}
