namespace Postech8SOAT.FastOrder.Types.Results;

public class Result<TValue>
{
    private Result(TValue value)
    {
        Value = value;
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

    public static Result<TValue> Succeed(TValue value) => new(value);
    public static Result<TValue> Failure(AppProblemDetails details) => new([details]);
    public static Result<TValue> Failure(List<AppProblemDetails> details) => new(details);
    public static Result<TValue> Empty() => new();
}
