using Moq;
using Postech8SOAT.FastOrder.Domain.Entities.Enums;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Pagamentos.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.UseCases.Abstractions.Gateways; 
public class IFornecedorPagamentoGatewayTest
{

    [Fact]
    public async Task IniciarPagamento_DeveRetornarPagamentoComIdExterno()
    {
        // Arrange
        var fornecedorPagamentoGateway = new FornecedorPagamentoGateway();
        var metodoDePagamento = MetodoDePagamento.Pix;
        var emailPagador = "pagador@dominio.com";
        var valorTotal = 100.0m;
        var referenciaExternaId = "ref-12345";
        var pedidoId = Guid.NewGuid();

        // Act
        var response = await fornecedorPagamentoGateway.IniciarPagamento(
            metodoDePagamento, emailPagador, valorTotal, referenciaExternaId, pedidoId);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.IdExterno);
        Assert.StartsWith("https://pagamento.com/", response.UrlPagamento);  // Verifica o formato da URL
    }

    [Fact]
    public async Task ObterPagamento_DeveRetornarPagamentoQuandoExistir()
    {
        // Arrange
        var fornecedorPagamentoGateway = new FornecedorPagamentoGateway();
        var metodoDePagamento = MetodoDePagamento.Pix;
        var emailPagador = "pagador@dominio.com";
        var valorTotal = 100.0m;
        var referenciaExternaId = "ref-12345";
        var pedidoId = Guid.NewGuid();
        var response = await fornecedorPagamentoGateway.IniciarPagamento(
            metodoDePagamento, emailPagador, valorTotal, referenciaExternaId, pedidoId);

        // Act
        var pagamento = await fornecedorPagamentoGateway.ObterPagamento(response.IdExterno);

        // Assert
        Assert.NotNull(pagamento);
        Assert.Equal(response.IdExterno, pagamento?.IdExterno);
    }

    [Fact]
    public async Task ObterPagamento_DeveRetornarNullQuandoPagamentoNaoExistir()
    {
        // Arrange
        var fornecedorPagamentoGateway = new FornecedorPagamentoGateway();
        var idExternoInexistente = "id-inexistente";

        // Act
        var pagamento = await fornecedorPagamentoGateway.ObterPagamento(idExternoInexistente);

        // Assert
        Assert.Null(pagamento);
    }

}



public class FornecedorPagamentoGateway : IFornecedorPagamentoGateway
{
    private readonly Dictionary<string, FornecedorGetPagamentoResponseDto> _pagamentos;

    public FornecedorPagamentoGateway()
    {
        _pagamentos = new Dictionary<string, FornecedorGetPagamentoResponseDto>();
    }

    public Task<FornecedorCriarPagamentoResponseDto> IniciarPagamento(
        MetodoDePagamento metodoDePagamento,
        string emailPagador,
        decimal valorTotal,
        string referenciaExternaId,
        Guid pedidoId,
        CancellationToken cancellationToken = default)
    {
        // Simula a criação de um pagamento
        var pagamentoId = Guid.NewGuid().ToString();
        var response = new FornecedorCriarPagamentoResponseDto(pagamentoId, $"https://pagamento.com/{pagamentoId}");

        // Armazena o pagamento para simulação de consulta posterior
        _pagamentos[pagamentoId] = new FornecedorGetPagamentoResponseDto(pagamentoId, pedidoId, StatusPagamento.Pendente);

        return Task.FromResult(response);
    }

    public Task<FornecedorGetPagamentoResponseDto> ObterPagamento(
        string idExterno,
        CancellationToken cancellationToken = default)
    {
        _pagamentos.TryGetValue(idExterno, out var pagamento);
        return Task.FromResult(pagamento);
    }


    [Fact]
    public async Task CriarPagamentoAsync_DeveRetornarIdExterno()
    {
        // Arrange
        var mockFornecedorPagamentoGateway = new Mock<IFornecedorPagamentoGateway>();
        var metodoDePagamento = MetodoDePagamento.Pix;
        var emailPagador = "pagador@dominio.com";
        var valorTotal = 100.0m;
        var referenciaExternaId = "ref-12345";
        var pedidoId = Guid.NewGuid();
        var expectedResponse = new FornecedorCriarPagamentoResponseDto("12345", "https://pagamento.com/12345");
        mockFornecedorPagamentoGateway.Setup(gateway => gateway.IniciarPagamento(
            metodoDePagamento, emailPagador, valorTotal, referenciaExternaId, pedidoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        var pagamentoService = new PagamentoService(mockFornecedorPagamentoGateway.Object);

        // Act
        var response = await pagamentoService.CriarPagamentoAsync(metodoDePagamento, emailPagador, valorTotal, referenciaExternaId, pedidoId);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("12345", response.IdExterno);
        Assert.StartsWith("https://pagamento.com/", response.UrlPagamento);
    }

    [Fact]
    public async Task ConsultarPagamentoAsync_DeveRetornarPagamentoQuandoExistir()
    {
        // Arrange
        var mockFornecedorPagamentoGateway = new Mock<IFornecedorPagamentoGateway>();
        var idExterno = "12345";
        var expectedPayment = new FornecedorGetPagamentoResponseDto(idExterno, Guid.NewGuid(), StatusPagamento.Pendente);
        mockFornecedorPagamentoGateway.Setup(gateway => gateway.ObterPagamento(idExterno, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPayment);

        var pagamentoService = new PagamentoService(mockFornecedorPagamentoGateway.Object);

        // Act
        var payment = await pagamentoService.ConsultarPagamentoAsync(idExterno);

        // Assert
        Assert.NotNull(payment);
        Assert.Equal(idExterno, payment?.IdExterno);
    }

    [Fact]
    public async Task ConsultarPagamentoAsync_DeveRetornarNullQuandoNaoExistir()
    {
        // Arrange
        var mockFornecedorPagamentoGateway = new Mock<IFornecedorPagamentoGateway>();
        var idExternoInexistente = "id-inexistente";
        mockFornecedorPagamentoGateway.Setup(gateway => gateway.ObterPagamento(idExternoInexistente, It.IsAny<CancellationToken>()))
            .ReturnsAsync((FornecedorGetPagamentoResponseDto?)null);

        var pagamentoService = new PagamentoService(mockFornecedorPagamentoGateway.Object);

        // Act
        var payment = await pagamentoService.ConsultarPagamentoAsync(idExternoInexistente);

        // Assert
        Assert.Null(payment);
    }
}


public class PagamentoService
{
    private readonly IFornecedorPagamentoGateway _fornecedorPagamentoGateway;

    public PagamentoService(IFornecedorPagamentoGateway fornecedorPagamentoGateway)
    {
        _fornecedorPagamentoGateway = fornecedorPagamentoGateway;
    }

    public async Task<FornecedorCriarPagamentoResponseDto> CriarPagamentoAsync(
        MetodoDePagamento metodoDePagamento,
        string emailPagador,
        decimal valorTotal,
        string referenciaExternaId,
        Guid pedidoId,
        CancellationToken cancellationToken = default)
    {
        return await _fornecedorPagamentoGateway.IniciarPagamento(
            metodoDePagamento, emailPagador, valorTotal, referenciaExternaId, pedidoId, cancellationToken);
    }

    public async Task<FornecedorGetPagamentoResponseDto> ConsultarPagamentoAsync(
        string idExterno,
        CancellationToken cancellationToken = default)
    {
        return await _fornecedorPagamentoGateway.ObterPagamento(idExterno, cancellationToken);
    }
}
