using Bogus;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.Tests.Integration.Builder
{
    internal class PagamentoDTOBuilder : Faker<PagamentoDTO>
    {
        public PagamentoDTOBuilder()
        {
            CustomInstantiator(f => PagamentoDTO.Create(
                id: f.Random.Guid(),
                pedidoId: f.Random.Guid(),
                pagamentoExternoId: f.Random.Bool() ? f.Random.Guid().ToString() : null,
                status: f.PickRandom<StatusPagamento>(),
                metodoDePagamento: f.PickRandom<MetodoDePagamento>(),
                valorTotal: f.Finance.Amount(1, 1000) // Gera um valor aleatório entre 1 e 1000
            ));
        }

        // Método para definir um id específico
        public PagamentoDTOBuilder ComId(Guid id)
        {
            CustomInstantiator(f => PagamentoDTO.Create(
                id: id,
                pedidoId: f.Random.Guid(),
                pagamentoExternoId: f.Random.Bool() ? f.Random.Guid().ToString() : null,
                status: f.PickRandom<StatusPagamento>(),
                metodoDePagamento: f.PickRandom<MetodoDePagamento>(),
                valorTotal: f.Finance.Amount(1, 1000)
            ));
            return this;
        }

        // Método para definir o pedidoId específico
        public PagamentoDTOBuilder ComPedidoId(Guid pedidoId)
        {
            CustomInstantiator(f => PagamentoDTO.Create(
                id: f.Random.Guid(),
                pedidoId: pedidoId,
                pagamentoExternoId: f.Random.Bool() ? f.Random.Guid().ToString() : null,
                status: f.PickRandom<StatusPagamento>(),
                metodoDePagamento: f.PickRandom<MetodoDePagamento>(),
                valorTotal: f.Finance.Amount(1, 1000)
            ));
            return this;
        }

        // Método para definir o valor total
        public PagamentoDTOBuilder ComValorTotal(decimal valorTotal)
        {
            CustomInstantiator(f => PagamentoDTO.Create(
                id: f.Random.Guid(),
                pedidoId: f.Random.Guid(),
                pagamentoExternoId: f.Random.Bool() ? f.Random.Guid().ToString() : null,
                status: f.PickRandom<StatusPagamento>(),
                metodoDePagamento: f.PickRandom<MetodoDePagamento>(),
                valorTotal: valorTotal
            ));
            return this;
        }

        // Método para gerar o PagamentoDTO
        public PagamentoDTO Build() => Generate();
    }
}
