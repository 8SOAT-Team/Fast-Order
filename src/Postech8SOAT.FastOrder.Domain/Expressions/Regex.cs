using System.Text.RegularExpressions;

namespace Postech8SOAT.FastOrder.Domain.Expressions;

public partial class Expression
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public static partial Regex ValidEmail();

    [GeneratedRegex(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$|^[0-9]{11}$")]
    public static partial Regex HasCpfLength();

    [GeneratedRegex(@"[^0-9]")]
    public static partial Regex DigitsOnly();
}
