using System.Security.Claims;
using System.Security.Cryptography;
using LocadoraDeVeiculo.Context;
using LocadoraDeVeiculo.Dto;
using LocadoraDeVeiculo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
               await ValidarCadastro(veiculo, usuario, reservaModel);
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

        public List<ReservaModelDTO> HistoricoReserva(string userName, bool isAdmin)
        {
            var query = _context.Reservas.AsQueryable();

            if (isAdmin)
            {
                
                query = query.Where(r => r.Ativo);
            }
            else
            {
                query = query.Where(r => r.UserNameUsuario == userName);
            }

            return query
                .Select(r => new ReservaModelDTO
                {
                    IdVeiculo = r.IdVeiculo,
                    NomeVeiculo = r.NomeVeiculo,
                    UserNameUsuario = r.UserNameUsuario, 
                    DataInicio = r.DataInicio,
                    DataFim = r.DataFim,
                    DiasReservados = r.DiasReservados,
                    ValorTotal = r.ValorTotal,
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

        public async Task AtualizarSituacaoVeiculos()
        {
            var veiculos = await _context.Veiculos.ToListAsync();

            foreach (var veiculo in veiculos)
            {
                var reservasAtivas = await _context.Reservas
                    .Where(r => r.IdVeiculo == veiculo.id && r.Ativo)
                    .ToListAsync();

                var agora = DateTime.Now;

                
                if (reservasAtivas.Any(r => r.DataInicio <= agora && r.DataFim >= agora))
                {
                    veiculo.Situacao = "Alugado";
                }
                
                else if (reservasAtivas.Any(r => r.DataInicio > agora))
                {
                    veiculo.Situacao = "Reservado";
                }
                else
                {
                    veiculo.Situacao = "Disponível";
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task ValidarCadastro(VeiculoModel veiculo, IdentityUser usuario, ReservaModel reservaModel)
        {
            if (veiculo == null || usuario == null)
                throw new Exception("Veículo não encontrado ou usuário inválido.");

            if (veiculo.Situacao == "Alugado")
                throw new Exception("Veículo Indisponível!");

            if (reservaModel.DataFim <= reservaModel.DataInicio)
                throw new Exception("O período mínimo de reserva é de 1 dia.");

            var reservaExistente = await _context.Reservas.Where(r => r.IdVeiculo == veiculo.id && r.Ativo == true).ToListAsync();

            foreach (var reservaFuturas in reservaExistente)
            {
                if((reservaFuturas.DataInicio - reservaModel.DataFim).TotalDays < 2 || reservaModel.DataFim == reservaFuturas.DataInicio)
                {
                    throw new Exception("Esse veiculo já está reservado, deve haver pelo menos um intervalo de dois dias antes" + reservaFuturas.DataInicio.ToString("dd/MM/yyyy"));
                }
            }

        }

        private void CriarCadastro(ReservaModel reservaModel, VeiculoModel veiculo, IdentityUser usuario)
        {
            reservaModel.Id = 0;
            reservaModel.NomeVeiculo = veiculo.Marca + " - " + veiculo.Modelo;
            reservaModel.UserNameUsuario = usuario.UserName;
            reservaModel.Ativo = true;
            reservaModel.DiasReservados = (reservaModel.DataFim - reservaModel.DataInicio).Days;
            reservaModel.ValorTotal = reservaModel.DiasReservados * veiculo.ValorDiaria;
        }
    }
}
