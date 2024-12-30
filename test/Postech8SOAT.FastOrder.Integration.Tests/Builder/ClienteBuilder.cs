using Bogus;
using Bogus.Extensions.Brazil;
using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
public class ClienteBuilder : Faker<Cliente>
{
    public ClienteBuilder()
    {
        CustomInstantiator(f => new Cliente(f.Person.Cpf(), f.Person.FullName, f.Person.Email));
    }
    public Cliente Build() => Generate();
}
