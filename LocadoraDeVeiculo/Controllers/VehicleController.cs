using System.Threading.Tasks;
using LocadoraDeVeiculo.Models;
using LocadoraDeVeiculo.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LocadoraDeVeiculo.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VeiculoService _veiculoService;
        public VehicleController(VeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }



        
        public async Task<IActionResult> Index()
        {
           var listaVeiculos = await _veiculoService.ListVehicles();
            return View(listaVeiculos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VeiculoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _veiculoService.CreateVehicle(model);
            return RedirectToAction("Index", "Vehicle");
        }


    }
}
