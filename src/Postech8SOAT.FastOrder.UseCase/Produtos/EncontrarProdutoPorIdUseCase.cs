using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Common;

namespace Postech8SOAT.FastOrder.UseCases.Produtos;

public class EncontrarProdutoPorIdUseCase : UseCase<Guid, Produto>
{
    private readonly IProdutoGateway _produtoGateway;

    public EncontrarProdutoPorIdUseCase(ILogger logger, IProdutoGateway produtoGateway) : base(logger)
    {
        _produtoGateway = produtoGateway;
    }

    protected override async Task<Produto?> Execute(Guid id)
    {
        if(id == Guid.Empty)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Id do produto não pode ser vazio."));
            return null!;
        }

        return await _produtoGateway.GetProdutoByIdAsync(id);
    }
}