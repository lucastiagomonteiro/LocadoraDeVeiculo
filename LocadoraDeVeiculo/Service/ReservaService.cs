using System.Security.Claims;
using System.Security.Cryptography;
using LocadoraDeVeiculo.Context;
using LocadoraDeVeiculo.Dto;
using LocadoraDeVeiculo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculo.Service
{
    public class ReservaService : IReservaService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _user;
        
        public ReservaService(AppDbContext Context, UserManager<IdentityUser> User)
        {
            _context = Context;
            _user = User;
        }

        public async Task<ResultadoOperacaoDTO> CadastrarReserva(ReservaModel reservaModel, ClaimsPrincipal userPrincipal)
        {
            var veiculo = await _context.Veiculos.FindAsync(reservaModel.IdVeiculo);
            var usuario = await _user.GetUserAsync(userPrincipal);

            try
            {
                ValidarCadastro(veiculo, usuario, reservaModel);
            }
            catch (Exception ex)
            {
                return new ResultadoOperacaoDTO
                {
                    Sucesso = false,
                    Mensagem = ex.Message
                };
            }

            CriarCadastro(reservaModel, veiculo, usuario);

            await _context.Reservas.AddAsync(reservaModel);
            await _context.SaveChangesAsync();

            return new ResultadoOperacaoDTO
            {
                Sucesso = true,
                Mensagem = "Reserva realizada com sucesso!"
            };
        }

        public List<ReservaModelDTO> HistoricoReserva(string userName)
        {
            return _context.Reservas
                .Where(r => r.UserNameUsuario == userName)
                .Select(r => new ReservaModelDTO
                {
                    IdVeiculo = r.IdVeiculo,
                    NomeVeiculo = r.NomeVeiculo,
                    DataInicio = r.DataInicio,
                    DataFim = r.DataFim,
                    DiasReservados = r.DiasReservados,
                    ValorTotal = r.ValorTotal
                })
                .ToList();
        }

        public async Task DesativandoReservasVencidas()
        {
            var reservasAtivas = await _context.Reservas.Where(r => r.Ativo == true && r.DataFim <= DateTime.Now).ToListAsync();
            var veiculos = await _context.Veiculos.ToListAsync();
            foreach (var item in reservasAtivas)
            {

                item.Ativo = false;
                if (veiculos.Any())
                {
                    var veiculo = veiculos.FirstOrDefault(v => v.Situacao == "Alugado" && v.id == item.IdVeiculo);
                    if (veiculo != null)
                    {
                        
                        veiculo.Situacao = "Disponível";
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        private void ValidarCadastro(VeiculoModel veiculo, IdentityUser usuario, ReservaModel reservaModel)
        {
            if (veiculo == null || usuario == null)
                throw new Exception("Veículo não encontrado ou usuário inválido.");

            if (veiculo.Situacao == "Alugado")
                throw new Exception("Veículo Indisponível!");

            if (reservaModel.DataFim <= reservaModel.DataInicio)
                throw new Exception("O período mínimo de reserva é de 1 dia.");
        }

        private void CriarCadastro(ReservaModel reservaModel, VeiculoModel veiculo, IdentityUser usuario)
        {

            reservaModel.Id = 0;
            reservaModel.NomeVeiculo = veiculo.Marca + " - " + veiculo.Modelo;
            reservaModel.UserNameUsuario = usuario.UserName;
            reservaModel.Ativo = true;
            reservaModel.DiasReservados = (reservaModel.DataFim - reservaModel.DataInicio).Days;
            reservaModel.ValorTotal = reservaModel.DiasReservados * veiculo.ValorDiaria;

            veiculo.Situacao = "Alugado";
            veiculo.DataFinalAluguel = reservaModel.DataFim;
        }
    }
}
