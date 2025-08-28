using LocadoraDeVeiculo.Models;

namespace LocadoraDeVeiculo.Dto
{
    public class ReservaModelDTO
    {
        public int IdVeiculo { get; set; }
        public string NomeVeiculo { get; set; } = string.Empty;
        public string UserNameUsuario { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int DiasReservados { get; set; }
        public decimal ValorTotal { get; set; }
        public string StatusReserva => ObterStatusReserva(DataInicio, DataFim);

        private string ObterStatusReserva(DateTime inicio, DateTime fim)
        {
            var agora = DateTime.Now;

            if (inicio <= agora && fim >= agora)
                return "Alugado";
            else if (inicio > agora)
                return "Reservado";
            else
                return "Finalizada";
        }
    }
}
