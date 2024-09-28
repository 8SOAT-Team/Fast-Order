using Postech8SOAT.FastOrder.Types.Results;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints.Extensions;

public static class ResultExtension
{
    public static IResult GetResult<T>(this Result<T> result)
    {
        if (result.IsFailure)
        {
            return result.GetFailureResult();
        }

        return result.HasValue ? Results.Ok(result.Value) : Results.NotFound();
    }

    public static IResult GetFailureResult<T>(this Result<T> result)
    {
        var badRequestDetails = result.ProblemDetails.FirstOrDefault(x => x is AppBadRequestProblemDetails);
        return badRequestDetails is null ? Results.Problem() : Results.BadRequest(badRequestDetails);
    }
}