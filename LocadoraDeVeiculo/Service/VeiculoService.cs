using LocadoraDeVeiculo.Context;
using LocadoraDeVeiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculo.Service
{
    
    public class VeiculoService : IVeiculoService
    {
        private readonly AppDbContext _context;
        public VeiculoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateVehicle(VeiculoModel model)
        {
            await _context.Veiculos.AddAsync(model);
            await _context.SaveChangesAsync();

        }

        public Task DeleteVehicle(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditarVehicle(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VeiculoModel>> ListVehicle()
        {
            return await _context.Veiculos.Select(x => new VeiculoModel
            {
                Placa = x.Placa,
                Marca = x.Marca,
                Modelo = x.Modelo,
                Cor = x.Cor,
                Categoria = x.Categoria,
                Situacao = x.Situacao,
                ValorDiaria = x.ValorDiaria
            }).ToListAsync();
        }

        internal async Task<string?> ListVehicles()
        {
            throw new NotImplementedException();
        }
    }
}
