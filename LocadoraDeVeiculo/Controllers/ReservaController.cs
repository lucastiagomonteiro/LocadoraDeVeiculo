using System.Security.Claims;
using LocadoraDeVeiculo.Models;
using LocadoraDeVeiculo.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LocadoraDeVeiculo.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IReservaService _reservaService;

        public ReservaController(IVeiculoService veiculoService, ReservaService reservaService)
        {
            _veiculoService = veiculoService;
            _reservaService = reservaService;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var pegandoId = _veiculoService.BuscarPorId(id).Result;
            if (pegandoId == null)
            {
                return NotFound();
            }

            return View(pegandoId);
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(ReservaModel reservaModel, int id, ClaimsPrincipal userPrincipal)
        {
            var reserva = _reservaService.CadastrarReserva(reservaModel, id, userPrincipal);

            return View();
        }
    }
}
