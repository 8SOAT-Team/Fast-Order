using AutoMapper;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.WebAPI.Mapping;

public class DomainToDTOProfile:Profile
{
    public DomainToDTOProfile()
    {
        CreateMap<Produto, ProdutoDTO>()
            .ReverseMap();
            
        CreateMap<Cliente, ClienteDTO>()
            .ReverseMap();

        CreateMap<Categoria, CategoriaDTO>()
            .ReverseMap();

        CreateMap<Pedido, PedidoDTO>()
            .ReverseMap();

        
    }
}
