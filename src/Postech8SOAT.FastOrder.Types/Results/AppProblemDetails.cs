namespace Postech8SOAT.FastOrder.Types.Results;

public record AppProblemDetails(string Type, string Title, string Status, string Detail, string Instance);
public record AppBadRequestProblemDetails(string Type, string Detail, string Instance)
    : AppProblemDetails(Type, "Request invalido", StatusConst.BadRequest, Detail, Instance);