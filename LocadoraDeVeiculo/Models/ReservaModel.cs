using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LocadoraDeVeiculo.Models
{
    public class ReservaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NomeVeiculo = string.Empty;
        public string UserNameUsuario { get; set; } = string.Empty;
        public int IdVeiculo { get; set; } 
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int DiasReservados;
        public decimal ValorTotal { get; set; }
        public bool Ativo { get; set; } 
       
    }

}
