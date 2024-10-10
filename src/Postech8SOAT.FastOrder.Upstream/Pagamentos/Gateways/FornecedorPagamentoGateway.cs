using MercadoPago.Client;
using MercadoPago.Client.OAuth;
using MercadoPago.Client.Payment;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Upstream.Pagamentos.Gateways
{
    public class FornecedorPagamentoGateway(
        PaymentClient paymentClient,
        OAuthClient oAuthClient) : IFornecedorPagamentoGateway
    {
        private readonly PaymentClient _paymentClient = paymentClient;
        private readonly OAuthClient _oAuthClient = oAuthClient;
        public async Task<FornecedorCriarPagamentoResponseDto> IniciarPagamento(MetodoDePagamento metodoDePagamento, string emailPagador, decimal valorTotal, Guid pedidoId)
        {
            var options = await GetRequestOptions();

            var pagamentoRequest = new PaymentCreateRequest()
            {
                Installments = 1,
                PaymentMethod = new PaymentMethodRequest()
                {
                    Type = GetPaymentMethodType(metodoDePagamento),
                },
                Payer = new PaymentPayerRequest()
                {
                    Email = emailPagador,
                },
                TransactionAmount = valorTotal,
                ExternalReference = pedidoId.ToString(),
            };

            var response = await _paymentClient.CreateAsync(pagamentoRequest, options);

            return new FornecedorCriarPagamentoResponseDto(response.Id?.ToString()!);
        }

        public async Task<FornecedorGetPagamentoResponseDto> ObterPagamento(string IdExterno)
        {
            var options = await GetRequestOptions();
            var response = await _paymentClient.GetAsync(long.Parse(IdExterno));
            return new FornecedorGetPagamentoResponseDto(response.Id.ToString()!, GetStatusPagamento(response.Status));
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

        private async Task<string> GetAccessKey()
        {
            var request = new CreateOAuthCredentialRequest()
            {
                ClientId = "CLIENT_ID",
                ClientSecret = "CLIENT_secret",
            };
            var response = await _oAuthClient.CreateOAuthCredentialAsync(null, request.ClientId, request.ClientSecret, null);

            return response.AccessToken;
        }

        private async Task<RequestOptions> GetRequestOptions()
        {
            var accessToken = await GetAccessKey();
            var opt = new RequestOptions()
            {
                AccessToken = accessToken
            };
            opt.CustomHeaders.Add("x-idempotencyid", Guid.NewGuid().ToString());
            return opt;
        }
    }
}
