using AutoMapper;
using Postech8SOAT.FastOrder.Domain.Entities;
using Postech8SOAT.FastOrder.WebAPI.DTOs;

namespace Postech8SOAT.FastOrder.WebAPI.Mapping;

public class DomainToDTOProfile:Profile
{
    public DomainToDTOProfile()
    {
        CreateMap<Produto, ProdutoDTO>()
            .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.Categoria.Id))
            .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.Imagem))
            .ReverseMap();

        CreateMap<Cliente, ClienteDTO>()
            .ReverseMap();

        CreateMap<Categoria, CategoriaDTO>()
            .ReverseMap();

        CreateMap<Pedido, PedidoDTO>()
            .ReverseMap();
    }
}
