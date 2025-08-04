using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraDeVeiculo.Models
{
    public class VeiculoModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "A marca é obrigatória.")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; } = string.Empty;

        [Range(1900, 2100, ErrorMessage = "O ano deve ser entre 1900 e 2100.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "A cor é obrigatória.")]
        public string Cor { get; set; } = string.Empty;

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "A situação é obrigatória.")]
        public string Situacao { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "O valor da diária não pode ser negativo.")]
        public decimal ValorDiaria { get; set; }
        public string? ImagemUrl { get; set; }

        [NotMapped]
        public DateTime DataFinalAluguel { get; set; }

        [NotMapped]
        public IFormFile? ImagemUpload { get; set; }


    }
}
