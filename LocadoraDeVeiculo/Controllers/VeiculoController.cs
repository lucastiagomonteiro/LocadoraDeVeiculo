using System.Threading.Tasks;
using LocadoraDeVeiculo.Models;
using LocadoraDeVeiculo.Service;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Index()
        {
            var listaVeiculos = await _veiculoService.ListVehicle();
            return View(listaVeiculos);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Criar(VeiculoModel model, IFormFile ImagemUpload)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _veiculoService.CreateVehicle(model, ImagemUpload);
            return RedirectToAction("Index", "Veiculo");
        }
    }
}