using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Logging;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Gateways.Interfaces;
using Postech8SOAT.FastOrder.UseCases.Common;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;

namespace Postech8SOAT.FastOrder.UseCases.Produtos;

public class CriarProdutoUseCase : UseCase<NovoProdutoDTO, Produto>
{
    private readonly IProdutoGateway _produtoGateway;
    private readonly ICategoriaGateway _categoriaGateway;

    public CriarProdutoUseCase(ILogger logger, ICategoriaGateway categoriaGateway, IProdutoGateway produtoGateway) : base(logger)
    {
        _categoriaGateway = categoriaGateway;
        _produtoGateway = produtoGateway;
    }

    protected override async Task<Produto?> Execute(NovoProdutoDTO command)
    {
        var categoria = await _categoriaGateway.GetCategoriaByIdAsync(command.CategoriaId);

        if (categoria is null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "CategoriaId não existe"));
            return null;
        }

        var produto = new Produto(command.Nome, command.Descricao, command.Preco, command.Imagem, command.CategoriaId);
        var produtoCriado = await _produtoGateway.CreateProdutoAsync(produto);

        return produtoCriado;
    }
}
