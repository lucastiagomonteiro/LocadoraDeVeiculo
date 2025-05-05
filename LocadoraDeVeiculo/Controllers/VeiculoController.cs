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
            var listaVeiculos = await _veiculoService.ListVeiculo();
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

            await _veiculoService.CreateVeiculo(model, ImagemUpload);
            return RedirectToAction("Index", "Veiculo");
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<IActionResult> Editar()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Editar(int id)
        {
            if(ModelState.IsValid)
            {
                await _veiculoService.EditarVeiculo(id);
                return RedirectToAction(nameof(Index));
            }
            
            return NotFound();
        }
    }
}