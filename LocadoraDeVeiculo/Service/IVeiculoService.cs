using LocadoraDeVeiculo.Models;

namespace LocadoraDeVeiculo.Service
{
    public interface IVeiculoService
    {
        Task<List<VeiculoModel>> ListVeiculo();
        Task CreateVeiculo(VeiculoModel model, IFormFile ImagemUpload);
        Task DeleteVeiculo(int id);
        Task EditarVeiculo(int id);
    }
}
