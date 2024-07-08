using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Domain.Ports.Repository;
using Postech8SOAT.FastOrder.Domain.Ports.Service;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.WebAPI.Endpoints;

public static class ProdutoExtensions
{
    public static void AddEnPointProdutos(this WebApplication app)
    {
        app.MapGet("/produto", async ([FromServices]IMapper mapper, [FromServices]IProdutoService service) =>
        {
           var produtosDTO = mapper.Map<ICollection<Produto>, ICollection<ProdutoDTO>>(await service.GetAllProdutosAsync());

            return Results.Ok(await Task.FromResult(produtosDTO));

        }).WithTags("Produto").WithSummary("Listagem de produtos cadastrados.").WithOpenApi();

     
        app.MapGet("/produto/{categoriaId}", async([FromServices]IMapper mapper, [FromServices]IProdutoService service, Guid categoriaId) =>
        {
            var produtosDTO = mapper.Map<ICollection<Produto>, ICollection<ProdutoDTO>>(await service.GetProdutosByCategoria(categoriaId));

            return Results.Ok(await Task.FromResult(produtosDTO));

        }).WithTags("Produto").WithSummary("Listagem de produtos por categoria.").WithOpenApi();
    }
}
