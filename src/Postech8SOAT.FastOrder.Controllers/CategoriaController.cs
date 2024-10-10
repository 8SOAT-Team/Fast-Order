using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Presenters.Produtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers;
public class CategoriaController : ICategoriaController
{
        private readonly ICategoriaGateway _categoriaGateway;

    public CategoriaController(ICategoriaGateway categoriaGateway)
    {
        _categoriaGateway = categoriaGateway;
    }

    public async Task<Result<ICollection<ProdutoCategoriaDTO>>> GetAllCategoriasAsync()
    {
        var categorias = await _categoriaGateway.GetAllCategoriasAsync();
        return Result<ICollection<ProdutoCategoriaDTO>>.Succeed(CategoriaAdapter.AdaptCategoria(categorias));
    }
}
