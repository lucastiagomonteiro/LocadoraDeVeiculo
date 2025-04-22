using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculo.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "O email é obrigatorio")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatório")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Lembre-me")]
        public bool RememberMe { get; set; }
    }
}
