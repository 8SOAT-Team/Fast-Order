using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Presenters.Produtos;
using Postech8SOAT.FastOrder.Controllers.Problems;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.Presenters.Clientes;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Produtos;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;

namespace Postech8SOAT.FastOrder.Controllers;
public class ProdutoController : IProdutoController
{
    private readonly IProdutoUseCase _produtoUseCase;
    private readonly ICategoriaUseCase _categoriaUseCase;

    private readonly ILogger _logger;
    private readonly IProdutoGateway _produtoGateway;
    private readonly ICategoriaGateway _categoriaGateway;

    public ProdutoController(IProdutoUseCase produtoUseCase,
     ICategoriaUseCase categoriaUseCase,
     ILogger logger,
     IProdutoGateway produtoGateway,
     ICategoriaGateway categoriaGateway)
    {
        _produtoUseCase = produtoUseCase;
        _categoriaUseCase = categoriaUseCase;
        _logger = logger;
        _produtoGateway = produtoGateway;
        _categoriaGateway = categoriaGateway;
    }

    public async Task<Result<ProdutoCriadoDTO>> CreateProdutoAsync(NovoProdutoDTO novoProduto)
    {
        var useCase = new CriarProdutoUseCase(_logger, _categoriaGateway, _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync(novoProduto);

        if (useCase.IsFailure)
        {
            var errors = useCase.GetErrors();
            var firstError = errors.First();
            var isBadRequest = firstError.Code == CleanArch.UseCase.Faults.UseCaseErrorType.BadRequest;

            if (isBadRequest)
            {
                return Result<ProdutoCriadoDTO>.Failure(new AppBadRequestProblemDetails(firstError.Description, novoProduto.Nome));
            }

            return Result<ProdutoCriadoDTO>.Failure(errors.AdaptUseCaseErrors().ToList());
        }

        return useCaseResult.HasValue ? Result<ProdutoCriadoDTO>.Succeed(ProdutoPresenter.AdaptProdutoCriado(useCaseResult.Value!))
            : Result<ProdutoCriadoDTO>.Empty();
    }

    public async Task DeleteProdutoAsync(Produto produto)
    {
        await _produtoUseCase.DeleteProdutoAsync(produto);
    }


    public async Task<ICollection<Produto>> GetAllProdutosAsync()
    {
        return await _produtoUseCase.GetAllProdutosAsync();
    }

    public Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        return _produtoUseCase.GetProdutoByIdAsync(id);
    }

    public Task<Produto?> GetProdutoByNomeAsync(string nome)
    {
        return _produtoUseCase.GetProdutoByNomeAsync(nome);
    }

    public Task<ICollection<Produto>> GetProdutosByCategoria(Guid categoriaId)
    {
        return _produtoUseCase.GetProdutosByCategoria(categoriaId);
    }

    public Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId)
    {
        return _categoriaUseCase.GetCategoriaByIdAsync(categoriaId);
    }

    public async Task<Produto> UpdateProdutoAsync(Produto produto)
    {
        await _produtoUseCase.UpdateProdutoAsync(produto);
        return produto;
    }

}
