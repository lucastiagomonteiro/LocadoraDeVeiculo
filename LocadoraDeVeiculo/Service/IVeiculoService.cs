using LocadoraDeVeiculo.Models;

namespace LocadoraDeVeiculo.Service
{
    public interface IVeiculoService
    {
        Task<List<VeiculoModel>> ListVehicle();
        Task CreateVehicle(VeiculoModel model);
        Task DeleteVehicle(int id);
        Task EditarVehicle(int id);
    }
}
