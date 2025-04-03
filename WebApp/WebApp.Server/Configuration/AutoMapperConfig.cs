using App.Core.Business.Models;
using AutoMapper;
using WebApp.Server.ViewModels;

namespace WebApp.Server.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<PedidoViewModel, Pedido>().ReverseMap();
            CreateMap<PedidoItem, PedidoItemViewModel>()
                .ForMember(dest => dest.ProdutoId, opt => opt.MapFrom(src => src.ProdutoId))
                .ReverseMap()
                    .ForMember(dest => dest.ProdutoId, opt => opt.MapFrom(src => src.ProdutoId));
        }
    }
}
