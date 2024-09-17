using Postech8SOAT.FastOrder.Types.Results;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints.Extensions;

public static class ResultExtension
{
    public static IResult GetResult<T>(this Result<T> result)
    {
        if (result.IsFailure)
        {
            var badRequestDetails = result.ProblemDetails.FirstOrDefault(x => x is AppBadRequestProblemDetails);
            return badRequestDetails is null ? Results.Problem() : Results.BadRequest(badRequestDetails);
        }

        return Results.Ok(result.Value);
    }
}
