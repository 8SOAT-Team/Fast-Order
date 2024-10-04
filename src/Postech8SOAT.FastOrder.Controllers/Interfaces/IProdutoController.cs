using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers.Interfaces;
public interface IProdutoController
{
    Task<Result<ProdutoDTO?>> GetProdutoByIdAsync(Guid id);
    Task<Result<ProdutoCriadoDTO>> CreateProdutoAsync(NovoProdutoDTO produto);
    Task<Result<ICollection<ProdutoDTO>>> GetAllProdutosAsync();
    Task<Result<ICollection<ProdutoDTO>>> ListarProdutoPorCategoriaAsync(Guid categoriaId);
}
