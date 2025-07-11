using System.Security.Claims;
using LocadoraDeVeiculo.Context;
using LocadoraDeVeiculo.Dto;
using LocadoraDeVeiculo.Models;
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

        public async Task<bool> CadastrarReserva(ReservaModel reservaModel, ClaimsPrincipal userPrincipal)
        {
            var veiculo = await _context.Veiculos.FindAsync(reservaModel.IdVeiculo);
            var usuario = await _user.GetUserAsync(userPrincipal);

            if (veiculo == null || usuario == null)
            {
                return false;
            }

            reservaModel.Id = 0;
            reservaModel.NomeVeiculo = veiculo.Marca + veiculo.Modelo;
            reservaModel.UserNameUsuario = usuario.UserName;
            reservaModel.Ativo = true;
            reservaModel.DiasReservados = (reservaModel.DataFim - reservaModel.DataInicio).Days;
            reservaModel.ValorTotal = reservaModel.DiasReservados * veiculo.ValorDiaria;

            veiculo.Situacao = "Alugado";

            await _context.Reservas.AddAsync(reservaModel);
            await _context.SaveChangesAsync();
            return true;
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
    }
}
