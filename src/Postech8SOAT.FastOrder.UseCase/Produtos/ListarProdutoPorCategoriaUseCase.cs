using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Common;

namespace Postech8SOAT.FastOrder.UseCases.Produtos;

public class ListarProdutoPorCategoriaUseCase : UseCase<Guid, ICollection<Produto>>
{
    private readonly IProdutoGateway _produtoGateway;

    public ListarProdutoPorCategoriaUseCase(ILogger logger, IProdutoGateway produtoGateway) : base(logger)
    {
        _produtoGateway = produtoGateway;
    }

    protected override async Task<ICollection<Produto>?> Execute(Guid command)
    {
        if (command == Guid.Empty)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "CategoriaId não pode ser vazio"));
            return null!;
        }

        return await _produtoGateway.GetProdutosByCategoriaAsync(command);
    }
}
