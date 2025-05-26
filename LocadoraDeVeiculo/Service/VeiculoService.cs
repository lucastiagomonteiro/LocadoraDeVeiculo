using LocadoraDeVeiculo.Context;
using LocadoraDeVeiculo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LocadoraDeVeiculo.Service
{
    
    public class VeiculoService : IVeiculoService
    {
        private readonly AppDbContext _context;
        public VeiculoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VeiculoModel> BuscarPorId(int id)
        {
            return await _context.Veiculos.FirstOrDefaultAsync(v => v.id == id);
        }

        public async Task CriarVeiculo(VeiculoModel model, IFormFile ImagemUpload)
        {   

            model.ImagemUrl = await AddImagemVeiculo(model, ImagemUpload);
            await _context.Veiculos.AddAsync(model);
            await _context.SaveChangesAsync();

        }

        public async Task ApagarVeiculo(int id)
        {
            var contatoId = await _context.Veiculos.FindAsync(id);
            if (contatoId !=  null)
            {
                _context.Veiculos.Remove(contatoId);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditarVeiculo(int id, VeiculoModel model, IFormFile ImagemUpload)
        {
            var veiculoExistente = await _context.Veiculos.FindAsync(id);
            if (veiculoExistente != null)
            {
                // Atualiza os campos normais
                veiculoExistente.Placa = model.Placa;
                veiculoExistente.Marca = model.Marca;
                veiculoExistente.Modelo = model.Modelo;
                veiculoExistente.Ano = model.Ano;
                veiculoExistente.Cor = model.Cor;
                veiculoExistente.Categoria = model.Categoria;
                veiculoExistente.Situacao = model.Situacao;
                veiculoExistente.ValorDiaria = model.ValorDiaria;

                if(ImagemUpload != null && ImagemUpload.Length > 0)
                {
                    veiculoExistente.ImagemUrl = await AddImagemVeiculo(model, ImagemUpload);              
                }

                _context.Veiculos.Update(veiculoExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<VeiculoModel>> ListVeiculo()
        {
            return await _context.Veiculos.Select(x => new VeiculoModel
            {
                id = x.id,
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

        public async Task<string> AddImagemVeiculo(VeiculoModel model, IFormFile ImagemUpload)
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

                return "/Imagens/" + nomeImagem;
            }

            return null;
        }
    }
}
