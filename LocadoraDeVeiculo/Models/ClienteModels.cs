using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculo.Models
{
    public class ClienteModels
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string CNH { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string IdentityUserId { get; set; } = string.Empty;

    }
}
