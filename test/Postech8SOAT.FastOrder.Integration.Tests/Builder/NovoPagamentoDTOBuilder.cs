using Bogus;
using Postech8SOAT.FastOrder.Controllers.Pagamentos.Enums;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Integration.Tests.Builder;
internal class NovoPagamentoDTOBuilder:Faker<NovoPagamentoDTO>
{
    public NovoPagamentoDTOBuilder()
    {
        CustomInstantiator(f => new NovoPagamentoDTO()
        {
           MetodoDePagamento = MetodosDePagamento.Pix
        });
    }
    public NovoPagamentoDTO Build() => Generate();
}
