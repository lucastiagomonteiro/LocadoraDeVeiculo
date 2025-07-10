using LocadoraDeVeiculo.Models;

namespace LocadoraDeVeiculo.Dto
{
    public class ReservaModelDTO
    {
        public int IdVeiculo { get; set; }
        public string NomeVeiculo { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int DiasReservados { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
