using Bogus;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Tests.Integration.Builder;
public class CategoriaBuilder : Faker<CategoriaDTO>
{

    public CategoriaBuilder()
    {
        CustomInstantiator(f => new CategoriaDTO(RetornaIdCategoriaUtil.RetornaIdCategoria(), f.Commerce.Categories(1).ToString()!, f.Commerce.Categories(1).ToString()!));
    }
    public CategoriaDTO Build() => Generate();
}
