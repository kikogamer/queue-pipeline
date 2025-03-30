using App.Core.Interfaces;
using App.Core.Models;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Server.Controllers.v1;
using WebApp.Server.ViewModels;

namespace WebApp.Server.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pedidos")]
    public class PedidosController : Controller
    {
        private readonly ILogger<ProdutosController> _logger;
        private readonly IMapper _mapper;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosController(ILogger<ProdutosController> logger, IMapper mapper, IPedidoRepository pedidoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Create(PedidoViewModel pedidoViewModel)
        {
            var pedido = _mapper.Map<Pedido>(pedidoViewModel);
            await _pedidoRepository.Add(pedido);
            return CreatedAtAction(nameof(Get), new { Id = pedido.Id }, pedido);
        }

        [HttpGet("{id:guid}")]
        public async Task<PedidoViewModel> Get(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.Get(id));
        }
    }
}
