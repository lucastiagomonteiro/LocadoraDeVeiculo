using System.Security.Claims;
using LocadoraDeVeiculo.Models;

namespace LocadoraDeVeiculo.Service
{
    public interface IReservaService
    {
        Task CadastrarReserva(ReservaModel reservaModel, int id, ClaimsPrincipal userPrincipal);
        List<ReservaModel> HistoricoReserva(ReservaModel model);
    }
}
