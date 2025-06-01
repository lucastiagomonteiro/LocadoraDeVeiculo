using System.Security.Claims;
using LocadoraDeVeiculo.Context;
using LocadoraDeVeiculo.Models;
using Microsoft.AspNetCore.Identity;
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
        public async Task CadastrarReserva(ReservaModel reservaModel, int id, ClaimsPrincipal userPrincipal)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            var usuario = await _user.GetUserAsync(userPrincipal);

            if (veiculo == null || usuario == null)
            {
                return;
            }

            reservaModel.IdVeiculo = veiculo.id;
            reservaModel.UserNameUsuario = usuario.UserName;
            reservaModel.Ativo = true;
        }

        public List<ReservaModel> HistoricoReserva(ReservaModel model)
        {
            throw new NotImplementedException();
        }
    }
}
