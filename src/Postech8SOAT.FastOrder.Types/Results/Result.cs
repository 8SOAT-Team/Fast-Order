namespace Postech8SOAT.FastOrder.Types.Results;

public class Result<TValue>
{
    private Result(TValue value)
    {
        Value = value;
        HasValue = true;
    }

    private Result(List<AppProblemDetails> details)
    {
        ProblemDetails = details;
    }

    private Result() { }

    public TValue? Value { get; private init; }

    public List<AppProblemDetails> ProblemDetails { get; private init; } = [];

    public bool IsFailure => ProblemDetails.Count > 0;

    public bool IsSucceed => !IsFailure;

    public bool HasValue { get; }

    public static Result<TValue> Succeed(TValue value) => new(value);
    public static Result<TValue> Failure(AppProblemDetails details) => new([details]);
    public static Result<TValue> Failure(List<AppProblemDetails> details) => new(details);
    public static Result<TValue> Empty() => new();
}

public static class ResultExtension
{
    public static void Match<T>(this Result<T> result, Action<T> onSuccess,
        Action<List<AppProblemDetails>> onFailure)
    {
        if (result.HasValue)
        {
            onSuccess(result.Value!);
            return;
        }

        onFailure(result.ProblemDetails);
    }
}
