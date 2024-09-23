using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Postech8SOAT.FastOrder.Domain.Entities;

namespace Postech8SOAT.FastOrder.Gateways.Interfaces;
public interface IProdutoGateway
{
    Task<Produto?> GetProdutoByIdAsync(Guid id);
    Task<Produto?> GetProdutoByNomeAsync(string nome);
    Task<Produto> CreateProdutoAsync(Produto produto);
    Task<Produto> UpdateProdutoAsync(Produto produto);
    Task DeleteProdutoAsync(Produto produto);
    Task<ICollection<Produto>> GetAllProdutosAsync();
    Task<ICollection<Produto>> GetProdutosByCategoria(Guid categoriaId);
    Task<Categoria?> FindCategoriaByIdAsync(Guid categoriaId);    
}