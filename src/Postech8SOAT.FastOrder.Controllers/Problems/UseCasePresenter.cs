using CleanArch.UseCase.Faults;
using Postech8SOAT.FastOrder.Types.Results;

namespace Postech8SOAT.FastOrder.Controllers.Problems;
public static class UseCasePresenter
{
    public static IEnumerable<AppProblemDetails> AdaptUseCaseErrors(this IEnumerable<UseCaseError> errors, string? title = null, string? useCaseName = null, string? entityId = null)
     => errors.Select(e => new AppProblemDetails(title!,
         string.IsNullOrEmpty(useCaseName) ? "Erro ao executar caso de uso" : $"Erro ao executar caso de uso {useCaseName}",
         e.Code.ToString(),
         e.Description,
         entityId!));

    public static AppProblemDetails AdaptUseCaseError(this UseCaseError error, string? title = null, string? useCaseName = null, string? entityId = null)
     => new(title!,
         string.IsNullOrEmpty(useCaseName) ? "Erro ao executar caso de uso" : $"Erro ao executar caso de uso {useCaseName}",
         error.Code.ToString(),
         error.Description,
         entityId!);
}

