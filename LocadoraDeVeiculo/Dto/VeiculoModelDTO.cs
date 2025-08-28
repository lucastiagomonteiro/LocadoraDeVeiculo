namespace LocadoraDeVeiculo.Dto
{
    public class VeiculoModelDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Cor { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public decimal ValorDiaria { get; set; }
        public string? ImagemUrl { get; set; }

        public DateTime? DataFimAluguel { get; set; } // quando está alugado
        public DateTime? ProximaReserva { get; set; } // quando ficará indisponível
    }
}
