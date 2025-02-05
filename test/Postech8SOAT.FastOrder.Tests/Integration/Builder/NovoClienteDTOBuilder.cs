using Bogus;
using Bogus.Extensions.Brazil;
using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;

namespace Postech8SOAT.FastOrder.Tests.Integration.Builder;
public class NovoClienteDTOBuilder : Faker<NovoClienteDto>
{
    public NovoClienteDTOBuilder()
    {
        CustomInstantiator(f => new NovoClienteDto(f.Person.Cpf(), f.Person.FullName, f.Person.Email));
    }

    public NovoClienteDto Build() => Generate();
}
