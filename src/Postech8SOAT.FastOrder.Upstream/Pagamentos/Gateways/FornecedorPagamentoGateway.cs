using MercadoPago.Client;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.Infra.Env;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Upstream.Pagamentos.Gateways
{
    public class FornecedorPagamentoGateway(PaymentClient paymentClient,
        PreferenceClient preferenceClient,
        IPedidoGateway pedidoGateway) : IFornecedorPagamentoGateway
    {
        private readonly PreferenceClient _preferenceClient = preferenceClient;
        private readonly PaymentClient _paymentClient = paymentClient;
        private readonly IPedidoGateway _pedidoGateway = pedidoGateway;

        public async Task<FornecedorCriarPagamentoResponseDto> IniciarPagamento(MetodoDePagamento metodoDePagamento,
            string emailPagador, decimal valorTotal, string referenciaExternaId, Guid pedidoId, CancellationToken cancellationToken = default)
        {
            var pedido = await _pedidoGateway.GetPedidoCompletoAsync(pedidoId);

            var options = GetRequestOptions();
            var preferenceRequest = new PreferenceRequest()
            {
                Items = pedido!.ItensDoPedido.Select(item => new PreferenceItemRequest()
                {
                    Id = item.Id.ToString(),
                    Title = item.Produto.Nome,
                    Description = item.Produto.Descricao,
                    Quantity = item.Quantidade,
                    UnitPrice = item.Produto.Preco,
                    CurrencyId = "BRL",
                }).ToList(),
                Payer = pedido.Cliente is null ? null : new PreferencePayerRequest()
                {
                    Email = emailPagador,
                    Name = pedido.Cliente.Nome,
                    Identification = new IdentificationRequest()
                    {
                        Type = "CPF",
                        Number = pedido.Cliente.Cpf,
                    },
                },
                BackUrls = new PreferenceBackUrlsRequest()
                {
                    Success = "https://fastorder.com.br/success",
                    Failure = "https://fastorder.com.br/failure",
                    Pending = "https://fastorder.com.br/pending",
                },
                NotificationUrl = EnvConfig.PagamentoWebhookUrl.AbsoluteUri,
                AutoReturn = "approved",
                ExternalReference = referenciaExternaId,
            };

            var response = await _preferenceClient.CreateAsync(preferenceRequest, options, cancellationToken);
            return new FornecedorCriarPagamentoResponseDto(response.Id?.ToString()!, response.InitPoint);
        }

        public async Task<FornecedorGetPagamentoResponseDto> ObterPagamento(string IdExterno, CancellationToken cancellationToken = default)
        {
            var options = GetRequestOptions();
            var response = await _paymentClient.GetAsync(long.Parse(IdExterno), options, cancellationToken);
            var pagamentoId = response.ExternalReference;

            return new FornecedorGetPagamentoResponseDto(response.Id.ToString()!, Guid.Parse(pagamentoId), GetStatusPagamento(response.Status));
        }

        private static string GetPaymentMethodType(MetodoDePagamento metodoDePagamento)
        => metodoDePagamento switch
        {
            MetodoDePagamento.Pix => "pix",
            MetodoDePagamento.Cartao or MetodoDePagamento.Master or MetodoDePagamento.Visa => "credit_card",
            _ => "pix",
        };

        private static StatusPagamento GetStatusPagamento(string status)
        => status switch
        {
            "approved" => StatusPagamento.Autorizado,
            "rejected" => StatusPagamento.Rejeitado,
            "pending" => StatusPagamento.Pendente,
            "cancelled" => StatusPagamento.Cancelado,
            _ => StatusPagamento.Pendente,
        };

        private RequestOptions GetRequestOptions()
        {
            var opt = new RequestOptions()
            {
                AccessToken = EnvConfig.PagamentoFornecedorAccessToken
            };
            opt.CustomHeaders.Add("x-idempotencyid", Guid.NewGuid().ToString());
            return opt;
        }
    }
}
