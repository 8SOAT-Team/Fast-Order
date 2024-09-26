namespace CleanArch.UseCase.Options;

public abstract record Any<T>
{
    public abstract T? Value { get; }

    public abstract bool HasValue { get; }

    public static Any<T> Some(T value) => new Some<T>(value);
    public static Any<T> Empty => new Empty<T>();
}

public record Some<T> : Any<T>
{
    public Some(T value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));

        Value = value;
    }

    public override T Value { get; }

    public override bool HasValue => true;
}

public record Empty<T> : Any<T>
{
    public override T Value => default!;

    public override bool HasValue => false;
}


public static class AnyExtension
{
    public static Any<T> ToAny<T>(this T? value) where T : class
    =>  value is null || (value is string s && string.IsNullOrEmpty(s)) ?
            Any<T>.Empty : Any<T>.Some(value);
}