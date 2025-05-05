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
        public async Task CreateVeiculo(VeiculoModel model, IFormFile ImagemUpload)
        {
            if (ImagemUpload != null && ImagemUpload.Length > 0)
            {
                var nomeImagem = Guid.NewGuid().ToString() + Path.GetExtension(ImagemUpload.FileName);
                var caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imagens", nomeImagem);

                var pastaImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imagens");
                if (!Directory.Exists(pastaImagens))
                {
                    Directory.CreateDirectory(pastaImagens);
                }

                using (var stream = new FileStream(caminhoImagem, FileMode.Create))
                {
                    await ImagemUpload.CopyToAsync(stream);
                }

                model.ImagemUrl = "/Imagens/" + nomeImagem;
            }

            
            await _context.Veiculos.AddAsync(model);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteVeiculo(int id)
        {
            var contatoId = await _context.Veiculos.FindAsync(id);
            if (contatoId !=  null)
            {
                _context.Veiculos.Remove(contatoId);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditarVeiculo(int id)
        {
            var contatoId = await _context.Veiculos.FindAsync(id);
            if (contatoId != null)
            {
                _context.Veiculos.Update(contatoId);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<VeiculoModel>> ListVeiculo()
        {
            return await _context.Veiculos.Select(x => new VeiculoModel
            {
                Placa = x.Placa,
                Marca = x.Marca,
                Modelo = x.Modelo,
                Ano = x.Ano,
                Cor = x.Cor,
                Categoria = x.Categoria,
                Situacao = x.Situacao,
                ValorDiaria = x.ValorDiaria,
                ImagemUrl = x.ImagemUrl
            }).ToListAsync();
        }
    }
}
