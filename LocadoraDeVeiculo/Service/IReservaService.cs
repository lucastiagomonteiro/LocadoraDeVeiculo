using System.Security.Claims;
using LocadoraDeVeiculo.Dto;
using LocadoraDeVeiculo.Models;

namespace LocadoraDeVeiculo.Service
{
    public interface IReservaService
    {
        Task<bool> CadastrarReserva(ReservaModel reservaModel, ClaimsPrincipal userPrincipal);
        List<ReservaModelDTO> HistoricoReserva(string userName);
    }
}
