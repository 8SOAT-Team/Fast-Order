﻿using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Postech8SOAT.FastOrder.Domain.ValueObjects;
using Postech8SOAT.FastOrder.UseCases.Abstractions.Gateways;

namespace Postech8SOAT.FastOrder.Gateways;
public class ProdutoGateway : IProdutoGateway
{
    private readonly ICategoriaGateway _categoriaGateway;
    private readonly FastOrderContext _dbContext;
    private readonly DbSet<Produto> _produtos;

    public ProdutoGateway(ICategoriaGateway categoriaGateway, FastOrderContext dbContext)
    {
        _categoriaGateway = categoriaGateway;
        _dbContext = dbContext;
        _produtos = dbContext.Set<Produto>();
    }

    public async Task<Produto> CreateProdutoAsync(Produto produto)
    {
        var insertedProduto = await _produtos.AddAsync(produto);
        await _dbContext.SaveChangesAsync();
        return insertedProduto.Entity;
    }

    public Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        const string query = "SELECT * FROM Produtos WHERE id = @id";
        return _produtos.FromSqlRaw(query, new SqlParameter("id", id))
            .FirstOrDefaultAsync();
    }


    public Task<Produto?> GetProdutoCompletoByIdAsync(Guid id)
    {
        return _dbContext.Set<Produto>().Include(x => x.Categoria)
             .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Produto>> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        return await _dbContext.Set<Produto>().Include(x => x.Categoria)
            .Where(x => x.CategoriaId == categoriaId)
            .ToListAsync();
    }

    public async Task<ICollection<Produto>> ListarProdutosByIdAsync(ICollection<Guid> ids)
    {
        return await _dbContext.Set<Produto>().Include(x => x.Categoria)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<ICollection<Produto>> ListarTodosProdutosAsync()
    {
        return await _dbContext.Set<Produto>().Include(x => x.Categoria).ToListAsync();
    }
}