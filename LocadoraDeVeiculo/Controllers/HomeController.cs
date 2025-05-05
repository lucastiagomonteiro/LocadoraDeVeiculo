using System.Diagnostics;
using System.Threading.Tasks;
using LocadoraDeVeiculo.Models;
using LocadoraDeVeiculo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVeiculoService _veiculoService;
        public HomeController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _veiculoService.ListVeiculo();
            return View(lista);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
