namespace Postech8SOAT.FastOrder.Types.Results;

public record AppProblemDetails(string Type, string Title, string Status, string Detail, string Instance)
{
    public AppProblemDetails(string Title, string Status, string Detail, string Instance) :
        this("", Title, Status, Detail, Instance)
    {
    }
}
public record AppBadRequestProblemDetails(string Type, string Detail, string Instance)
    : AppProblemDetails(Type, "Request invalido", StatusConst.BadRequest, Detail, Instance)
{
    public AppBadRequestProblemDetails(string Detail, string Instance) : this("", Detail, Instance)
    {
    }
}