using System.Threading.Tasks;
using LocadoraDeVeiculo.Models;
using LocadoraDeVeiculo.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LocadoraDeVeiculo.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculoController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        public async Task<IActionResult> Index()
        {
            var listaVeiculos = await _veiculoService.ListVehicle();
            return View(listaVeiculos);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(VeiculoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _veiculoService.CreateVehicle(model);
            return RedirectToAction("Index", "Veiculo");
        }
    }
}