using App.Core.Business.Contracts;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Server.ViewModels;

namespace WebApp.Server.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/produtos")]
    public class ProdutosController : Controller
    {
        private readonly ILogger<ProdutosController> _logger;
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(ILogger<ProdutosController> logger, IMapper mapper, IProdutoRepository produtoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        [HttpGet("{id:guid}")]
        public async Task<ProdutoViewModel> Get(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.Get(id));
        }

        [HttpGet]
        public async Task<List<ProdutoViewModel>> GetAll()
        {
            return _mapper.Map<List<ProdutoViewModel>>(await _produtoRepository.GetAllAsync());
        }
    }
}
