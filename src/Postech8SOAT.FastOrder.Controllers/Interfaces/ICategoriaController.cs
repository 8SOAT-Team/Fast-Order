using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers.Interfaces;
public interface ICategoriaController
{
    Task<Result<ICollection<ProdutoCategoriaDTO>>> GetAllCategoriasAsync();
}
