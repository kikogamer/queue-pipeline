using App.Core.Models;
using AutoMapper;
using WebApp.ViewModels;

namespace WebApp.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Produto, ProdutoViewModel>();
        }
    }
}
