using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers.Presenters.Produtos;

internal static class ProdutoPresenter
{
    internal static ProdutoCriadoDTO AdaptProdutoCriado(Produto produto)
    {
        return new ProdutoCriadoDTO
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            Imagem = produto.Imagem,
            Categoria = new ProdutoCategoriaDTO
            {
                Id = produto.Categoria!.Id,
                Nome = produto.Categoria.Nome!
            }
        };
    }
}
