using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers.Presenters.Produtos;

public static class CategoriaAdapter
{
    public static ICollection<ProdutoCategoriaDTO> AdaptCategoria(ICollection<Categoria> categorias) => categorias.Select(AdaptCategoria).ToList();

    public static ProdutoCategoriaDTO AdaptCategoria(Categoria categoria)
    {
        return new ProdutoCategoriaDTO
        {
            Id = categoria.Id,
            Nome = categoria.Nome!
        };
    }
}