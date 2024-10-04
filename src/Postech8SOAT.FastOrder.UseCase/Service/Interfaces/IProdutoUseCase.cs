using Postech8SOAT.FastOrder.Domain.Entities;
namespace Postech8SOAT.FastOrder.UseCases.Service.Interfaces;
public interface IProdutoUseCase
{
    Task<Produto?> GetProdutoByIdAsync(Guid id);
    Task<Produto?> GetProdutoByNomeAsync(string nome);
    Task<Produto> UpdateProdutoAsync(Produto produto);
    Task DeleteProdutoAsync(Produto produto);
    Task<ICollection<Produto>> GetAllProdutosAsync();
    Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId);
}
