using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace LocadoraDeVeiculo.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }

        public DbSet<Models.ClienteModels> Clientes { get; set; }

        public DbSet<Models.VeiculoModel> Veiculos { get; set; }

        public DbSet<Models.ReservaModel> Reservas { get; set; }
    }
}
