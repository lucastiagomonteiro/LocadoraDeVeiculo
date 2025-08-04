using LocadoraDeVeiculo.Models;

namespace LocadoraDeVeiculo.Service
{
    public interface IVeiculoService
    {
        Task<List<VeiculoModel>> ListVeiculo();
        Task CriarVeiculo(VeiculoModel model, IFormFile ImagemUpload);
        Task<VeiculoModel> BuscarPorId(int id);
        Task ApagarVeiculo(int id);
        Task EditarVeiculo(int id, VeiculoModel model);
    }
}
