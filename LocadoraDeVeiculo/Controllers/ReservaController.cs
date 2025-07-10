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

        public ReservaController(IVeiculoService veiculoService, IReservaService reservaService)
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
        public IActionResult Cadastro(int id)
        {
            var reserva = new ReservaModel
            {
                IdVeiculo = id
            };

            return View(reserva);
        }


        [HttpPost]
        public async Task<IActionResult> Cadastro(ReservaModel reservaModel)
        {
            bool result = await _reservaService.CadastrarReserva(reservaModel,User);
            if (result == false)
            {
                TempData["MensagemErro"] = "Veículo não encontrado ou usuário inválido.";
                return View(); //de uma olhada, ser vai ser so na view, ou vai para tela de login para efetuar o login 
            }

            return RedirectToAction(nameof(Index), new { id = reservaModel.IdVeiculo });
        }

        [HttpGet]
        public IActionResult Historico()
        {
            var userName = User.Identity.Name;
            var historico = _reservaService.HistoricoReserva(userName);
            return View(historico);
        }

    }
}
