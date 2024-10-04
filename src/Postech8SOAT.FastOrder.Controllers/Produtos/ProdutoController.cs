using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Controllers.Presenters.Produtos;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
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
