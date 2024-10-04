using CleanArch.UseCase.Logging;
using CleanArch.UseCase.Options;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Common;

namespace Postech8SOAT.FastOrder.UseCases.Produtos;

public class ListarTodosProdutosUseCase : UseCase<Empty<object>, ICollection<Produto>>
{
    private readonly IProdutoGateway _produtoGateway;

    public ListarTodosProdutosUseCase(ILogger logger, IProdutoGateway produtoGateway) : base(logger)
    {
        _produtoGateway = produtoGateway;
    }

    protected override async Task<ICollection<Produto>?> Execute(Empty<object> empty)
    {
        return await _produtoGateway.ListarTodosProdutosAsync();
    }
}
