using System.Text.RegularExpressions;

namespace Postech8SOAT.FastOrder.Domain.Expressions;

public partial class Expression
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public static partial Regex ValidEmail();

    [GeneratedRegex(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$|\d{11}")]
    public static partial Regex HasCpfLength();

    [GeneratedRegex(@"\D")]
    public static partial Regex DigitsOnly();
}
