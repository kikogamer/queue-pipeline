using System.Diagnostics;
using App.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IProdutoRepository produtoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<ProdutoViewModel>>(await _produtoRepository.GetAllAsync()));
        }
                
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
