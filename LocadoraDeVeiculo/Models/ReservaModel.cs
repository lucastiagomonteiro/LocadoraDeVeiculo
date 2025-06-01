namespace LocadoraDeVeiculo.Models
{
    public class ReservaModel
    {
        public int Id { get; set; }

        public string UserNameUsuario { get; set; } = string.Empty;// Obtido do usuário logado
        public int IdVeiculo { get; set; } // Ou FK para o veículo

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public decimal ValorTotal { get; set; }

        public bool Ativo { get; set; } // true = em aberto; false = finalizada
    }

}
