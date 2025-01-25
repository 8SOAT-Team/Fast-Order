using Postech8SOAT.FastOrder.Upstream.Pagamentos.MercadoPago.Dtos;

namespace Postech8SOAT.FastOrder.Domain.Tests.External.Pagamentos.MercadoPago.Dtos;

public class PagamentoWebhookDTOTest
{
    [Fact]
    public void PagamentoWebhookDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = 12345L;
        var action = "payment.created";
        var apiVersion = "v1";
        var data = new PagamentoWebhookDataDTO { Id = "67890" };
        var dateCreated = DateTime.UtcNow;
        var liveMode = true;
        var type = "payment";
        var userId = "user_123";

        // Act
        var dto = new PagamentoWebhookDTO
        {
            Id = id,
            Action = action,
            ApiVersion = apiVersion,
            Data = data,
            DateCreated = dateCreated,
            LiveMode = liveMode,
            Type = type,
            UserId = userId
        };

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(action, dto.Action);
        Assert.Equal(apiVersion, dto.ApiVersion);
        Assert.Equal(data, dto.Data);
        Assert.Equal(dateCreated, dto.DateCreated);
        Assert.Equal(liveMode, dto.LiveMode);
        Assert.Equal(type, dto.Type);
        Assert.Equal(userId, dto.UserId);
    }

    [Fact]
    public void PagamentoWebhookDataDTO_ShouldHaveCorrectProperties()
    {
        // Arrange
        var id = "67890";

        // Act
        var dataDto = new PagamentoWebhookDataDTO
        {
            Id = id
        };

        // Assert
        Assert.Equal(id, dataDto.Id);
    }
}