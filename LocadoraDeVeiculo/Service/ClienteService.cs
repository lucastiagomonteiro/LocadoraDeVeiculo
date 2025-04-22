using LocadoraDeVeiculo.Context;
using LocadoraDeVeiculo.Controllers;
using LocadoraDeVeiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculo.Service
{
    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(ClienteModels model)
        {
            await _context.Clientes.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
