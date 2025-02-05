using Bogus;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech8SOAT.FastOrder.Tests.Integration.Builder;
internal class NovoProdutoDTOInvalidoBuilder : Faker<NovoProdutoDTO>
{
    public NovoProdutoDTOInvalidoBuilder()
    {
        CustomInstantiator(f => new NovoProdutoDTO()
        {
            Nome = "",
            Descricao = f.Commerce.ProductDescription().Substring(0, 10),
            Preco = f.Random.Decimal(5, 1000),
            Imagem = f.Image.LoremFlickrUrl(),
            CategoriaId = RetornaIdCategoriaUtil.RetornaIdCategoria()
        });
    }
    public NovoProdutoDTO Build() => Generate();
}
