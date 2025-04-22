namespace LocadoraDeVeiculo.Service
{
    public interface ISeedUserRoleInitial
    {
        Task SeedRoleAsync();
        Task SeedUserRoleAsync();
    }
}
