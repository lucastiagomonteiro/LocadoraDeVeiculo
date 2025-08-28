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
        public async Task<IActionResult> Index(int id)
        {
            await _reservaService.DesativandoReservasVencidas();
            await _reservaService.AtualizarSituacaoVeiculos();


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

            var resultado = await _reservaService.CadastrarReserva(reservaModel,User);

            if (resultado.Sucesso == false)
            {
                TempData["MensagemErro"] = resultado.Mensagem;
                return View(); 
            }

            TempData["MensagemSucesso"] = resultado.Mensagem;
            return RedirectToAction(nameof(Index), new { id = reservaModel.IdVeiculo });

        }


        [HttpGet]
        public async Task<IActionResult> Historico()
        {
            var userName = User.Identity?.Name;
            bool isAdmin = User.IsInRole("Admin");

            var historico = _reservaService.HistoricoReserva(userName, isAdmin);
            return View(historico);
        }
    }
}
