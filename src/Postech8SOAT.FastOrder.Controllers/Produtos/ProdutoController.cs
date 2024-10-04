using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Presenters.Produtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Common;
using Postech8SOAT.FastOrder.UseCases.Produtos;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.Controllers;

public class ProdutoController : IProdutoController
{
    private readonly ILogger _logger;
    private readonly IProdutoGateway _produtoGateway;
    private readonly ICategoriaGateway _categoriaGateway;

    public ProdutoController(ILogger logger,
     IProdutoGateway produtoGateway,
     ICategoriaGateway categoriaGateway)
    {
        _logger = logger;
        _produtoGateway = produtoGateway;
        _categoriaGateway = categoriaGateway;
    }

    public async Task<Result<ProdutoCriadoDTO>> CreateProdutoAsync(NovoProdutoDTO novoProduto)
    {
        var useCase = new CriarProdutoUseCase(_logger, _categoriaGateway, _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync(novoProduto);

        return ControllerResultBuilder<ProdutoCriadoDTO, Produto>
            .ForUseCase(useCase)
            .WithInstance(novoProduto.Nome)
            .WithResult(useCaseResult)
            .AdaptUsing(ProdutoPresenter.AdaptProdutoCriado)
            .Build();
    }

    public async Task<Result<ICollection<ProdutoDTO>>> ListarProdutoPorCategoriaAsync(Guid categoriaId)
    {
        var useCase = new ListarProdutoPorCategoriaUseCase(_logger, _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync(categoriaId);

        return ControllerResultBuilder<ICollection<ProdutoDTO>, ICollection<Produto>>
            .ForUseCase(useCase)
            .WithInstance(categoriaId)
            .WithResult(useCaseResult)
            .AdaptUsing(ProdutoPresenter.AdaptProduto)
            .Build();
    }

    public async Task<Result<ICollection<ProdutoDTO>>> GetAllProdutosAsync()
    {
        var useCase = new ListarTodosProdutosUseCase(_logger, _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync();

        return ControllerResultBuilder<ICollection<ProdutoDTO>, ICollection<Produto>>
            .ForUseCase(useCase)
            .WithInstance("produtos")
            .WithResult(useCaseResult)
            .AdaptUsing(ProdutoPresenter.AdaptProduto)
            .Build();
    }

    public async Task<Result<ProdutoDTO?>> GetProdutoByIdAsync(Guid id)
    {
        var useCase = new EncontrarProdutoPorIdUseCase(_logger, _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync(id);

        return ControllerResultBuilder<ProdutoDTO?, Produto>
            .ForUseCase(useCase)
            .WithInstance(id)
            .WithResult(useCaseResult)
            .AdaptUsing(ProdutoPresenter.AdaptProduto)
            .Build();
    }
}
