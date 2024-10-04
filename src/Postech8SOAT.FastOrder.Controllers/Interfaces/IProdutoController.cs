using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers.Interfaces;
public interface IProdutoController
{
    Task<Produto?> GetProdutoByIdAsync(Guid id);
    Task<Produto?> GetProdutoByNomeAsync(string nome);
    Task<Result<ProdutoCriadoDTO>> CreateProdutoAsync(NovoProdutoDTO produto);
    Task<Produto> UpdateProdutoAsync(Produto produto);
    Task DeleteProdutoAsync(Produto produto);
    Task<ICollection<Produto>> GetAllProdutosAsync();
    Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId);

    Task<Result<ICollection<ProdutoDTO>>> ListarProdutoPorCategoriaAsync(Guid categoriaId);


}