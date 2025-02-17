using Bogus;
using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;

namespace Postech8SOAT.FastOrder.Tests.Integration.Builder;
internal class NovoClienteDTOBuilderInvalido : Faker<NovoClienteDto>
{
    public NovoClienteDTOBuilderInvalido()
    {
        CustomInstantiator(f => new NovoClienteDto("", f.Person.FullName, f.Person.Email));
    }
    public NovoClienteDto Build() => Generate();
}
