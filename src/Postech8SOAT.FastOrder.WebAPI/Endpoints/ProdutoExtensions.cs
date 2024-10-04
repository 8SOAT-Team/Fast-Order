using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;
using Postech8SOAT.FastOrder.WebAPI.Endpoints.Extensions;
using System.Net;
using ProdutoDTO = Postech8SOAT.FastOrder.UseCases.Produtos.Dtos.ProdutoDTO;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class ProdutoExtensions
{
    public static void AddEndPointProdutos(this WebApplication app)
    {
        const string ProdutoTag = "Produto";
        const string CategoriaTag = "Produto:Categoria";

        app.MapGet("/produto", async ([FromServices] IProdutoController controller) =>
        {
            var produtos = await controller.GetAllProdutosAsync();
            return produtos.GetResult();

        }).WithTags(ProdutoTag)
        .WithSummary("Obtenha a lista de todos os produtos cadastrados")
        .Produces<ICollection<ProdutoDTO>>((int)HttpStatusCode.OK)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();

        app.MapPost("/produto", async ([FromServices] IProdutoController controller, [FromBody] NovoProdutoDTO request) =>
        {
            var produtoCriado = await controller.CreateProdutoAsync(request);
            return produtoCriado.GetResult();
        }).WithTags(ProdutoTag)
        .WithSummary("Inclua novos produtos")
        .Produces<ProdutoCriadoDTO>((int)HttpStatusCode.OK)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();

        app.MapGet("/produto/categoria/{categoriaId}", async ([FromServices] IProdutoController controller, [FromRoute] Guid categoriaId) =>
        {
            var produtos = await controller.ListarProdutoPorCategoriaAsync(categoriaId);
            return produtos.GetResult();
        }).WithTags(CategoriaTag)
        .WithSummary("Liste todos os produtos de uma determinada categoria.")
        .Produces<ICollection<ProdutoDTO>>((int)HttpStatusCode.OK)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();

        app.MapGet("/produto/categoria", async ([FromServices] ICategoriaController controller) =>
        {
            

        }).WithTags(CategoriaTag).WithSummary("Obtenha a lista de todas as categorias.").WithOpenApi();

        app.MapGet("/produto/{id:guid}", async ([FromRoute] Guid id, [FromServices] IProdutoController controller) =>
        {
            var produto = await controller.GetProdutoByIdAsync(id);
            return produto.GetResult();
        }).WithTags(ProdutoTag)
        .WithSummary("Obtenha um produto pelo seu identificador.")
        .Produces<ProdutoDTO?>((int)HttpStatusCode.OK)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();       
    }
}
