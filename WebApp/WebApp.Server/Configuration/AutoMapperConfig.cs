using App.Core.Models;
using AutoMapper;
using WebApp.Server.ViewModels;

namespace WebApp.Server.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Produto, ProdutoViewModel>();
        }
    }
}
