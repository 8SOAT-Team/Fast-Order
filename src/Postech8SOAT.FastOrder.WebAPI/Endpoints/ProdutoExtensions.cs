using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Controllers.Clientes.Dtos;
using Postech8SOAT.FastOrder.Controllers.Interfaces;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Types.Results;
using Postech8SOAT.FastOrder.UseCases.Produtos.Dtos;
using Postech8SOAT.FastOrder.UseCases.Service.Interfaces;
using Postech8SOAT.FastOrder.WebAPI.DTOs;
using Postech8SOAT.FastOrder.WebAPI.Endpoints.Extensions;
using System.Net;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class ProdutoExtensions
{
    public static void AddEndPointProdutos(this WebApplication app)
    {
        const string ProdutoTag = "Produto";
        const string CategoriaTag = "Produto:Categoria";

        app.MapGet("/produto", async ([FromServices] IMapper mapper, [FromServices] IProdutoUseCase service) =>
        {
            var produtosDTO = mapper.Map<ICollection<Produto>, ICollection<ProdutoDTO>>(await service.GetAllProdutosAsync());

            return Results.Ok(await Task.FromResult(produtosDTO));

        }).WithTags(ProdutoTag).WithSummary("Listagem de produtos cadastrados.").WithOpenApi();

        app.MapGet("/produto/{id:guid}", async ([FromServices] IMapper mapper, [FromServices] IProdutoUseCase service, [FromRoute] Guid id) =>
        {
            var produto = await service.GetProdutoByIdAsync(id);

            if (produto is null)
            {
                return Results.NotFound("Produto não encontrado");
            }

            var produtosDTO = mapper.Map<Produto, ProdutoDTO>(produto);

            return Results.Ok(await Task.FromResult(produtosDTO));

        }).WithTags(ProdutoTag).WithSummary("Obtenha um produto pelo seu identificador.").WithOpenApi();

        app.MapPost("/produto", async ([FromServices] IProdutoController controller, [FromBody] NovoProdutoDTO request) =>
        {
            var produtoCriado = await controller.CreateProdutoAsync(request);
            return produtoCriado.GetResult();
        }).WithTags(ProdutoTag).WithSummary("Inclua novos produtos")
        .Produces<ProdutoCriadoDTO>((int)HttpStatusCode.OK)
        .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
        .Produces((int)HttpStatusCode.NotFound)
        .WithOpenApi();

        app.MapGet("/produto/categoria/{categoriaId}", async ([FromServices] IMapper mapper, [FromServices] IProdutoUseCase service, [FromRoute] Guid categoriaId) =>
        {
            var produtosDTO = mapper.Map<ICollection<Produto>, ICollection<ProdutoDTO>>(await service.GetProdutosByCategoria(categoriaId));

            return Results.Ok(await Task.FromResult(produtosDTO));

        }).WithTags(CategoriaTag).WithSummary("Listagem de produtos por categoria.").WithOpenApi();

        app.MapGet("/produto/categoria", async ([FromServices] IMapper mapper, [FromServices] ICategoriaController controller) =>
        {
            var categoriasDto = mapper.Map<ICollection<Categoria>, ICollection<CategoriaDTO>>(await controller.GetAllCategoriasAsync());

            return Results.Ok(await Task.FromResult(categoriasDto));

        }).WithTags(CategoriaTag).WithSummary("Listagem de categorias.").WithOpenApi();
    }
}
