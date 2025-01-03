using Microsoft.EntityFrameworkCore;
using Postech8SOAT.FastOrder.Integration.Tests.Builder;
using Postech8SOAT.FastOrder.Integration.Tests.HostTest;
using Postech8SOAT.FastOrder.WebAPI.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace Postech8SOAT.FastOrder.Integration.Tests;
public class FastOrder_ProdutoExtensionsTest : IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;
    public FastOrder_ProdutoExtensionsTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
    }
    [Fact]
    public async Task GET_Deve_buscar_produto_por_id()
    {
        //Arrange
        var produtoExistente = _factory.Context!.Produtos.FirstOrDefault();
        if (produtoExistente is null)
        {
            produtoExistente = new ProdutoBuilder().Build();
            _factory.Context.Add(produtoExistente);
            _factory.Context.SaveChanges();
        }
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.GetFromJsonAsync<ProdutoDTO>($"/produto/" + produtoExistente.Id);
        //Assert
        Assert.NotNull(response);
    }
    [Fact]
    public async Task POST_Deve_criar_produto()
    {
        //Arrange
        var produtoDto = new NovoProdutoDTOBuilder().Build();
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/produto", produtoDto);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Fact]
    public async Task POST_Nao_Deve_Criar_Produto_Com_Dados_Invalidos()
    {
        //Arrange
        var produtoDto = new NovoProdutoDTOInvalidoBuilder().Build();   
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/produto", produtoDto);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GET_Deve_Retornar_Produto_Id_Categoria()
    {
        //Arrange
        var produtoExistente = _factory.Context!.Produtos.FirstOrDefault();
        if (produtoExistente is null)
        {
            produtoExistente = new ProdutoBuilder().Build();
            _factory.Context.Add(produtoExistente);
            _factory.Context.SaveChanges();
        }
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.GetFromJsonAsync<ICollection<ProdutoDTO>>($"/produto/categoria/{produtoExistente.CategoriaId}");
        //Assert
        Assert.NotNull(response);
    }
    [Fact]

    public async Task GET_Nao_Deve_Retornar_Produto_Categoria_Id_Invalido()
    {
        //Arrange
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.GetFromJsonAsync<ICollection<ProdutoDTO>>($"/produto/categoria/{Guid.NewGuid()}");
        //Assert
        Assert.NotNull(response);
        Assert.Equal(0, response.Count);

    }

    [Fact]
    public async Task GET_Deve_Retornar_Lista_De_Categorias_Por_Produto()
    {
        //Arrange
        var categoriasExistente = await _factory.Context!.Categorias.ToListAsync();
        if (categoriasExistente is null)
        {
            categoriasExistente = new CategoriasBuilder().Build();
            _factory.Context.AddRange(categoriasExistente);
            _factory.Context.SaveChanges();
        }
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.GetFromJsonAsync<ICollection<UseCases.Produtos.Dtos.ProdutoCategoriaDTO>>($"/produto/categoria");
        //Assert
        Assert.NotNull(response);

    }

}
