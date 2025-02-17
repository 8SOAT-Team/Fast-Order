using CleanArch.UseCase.Faults;
using Postech8SOAT.FastOrder.Controllers.Problems;

namespace Postech8SOAT.FastOrder.Domain.Tests.Adapters.Controllers.Problemas;

public class UseCasePresenterTest
{
    [Fact]
    public void AdaptUseCaseErrors_ShouldReturnCorrectProblemDetails()
    {
        // Arrange
        var errors = new List<UseCaseError>
        {
            UseCaseErrorStubBuilder.Create(),
            UseCaseErrorStubBuilder.Create()
        };

        // Act
        var result = UseCasePresenter.AdaptUseCaseErrors(errors).ToList();

        // Assert
        Assert.Equal(errors.Count, result.Count);
        for (int i = 0; i < errors.Count; i++)
        {
            Assert.Equal($"Internal Error Description", result[i].Detail);
        }
    }

    [Fact]
    public void AdaptUseCaseError_ShouldReturnCorrectProblemDetails()
    {
        // Arrange
        var error = UseCaseErrorStubBuilder.Create();

        // Act
        var result = UseCasePresenter.AdaptUseCaseError(error);

        // Assert
        Assert.Equal($"Internal Error Description", result.Detail);
    }
}