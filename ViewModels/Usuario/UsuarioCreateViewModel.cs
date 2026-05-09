using System.ComponentModel.DataAnnotations;
using MecHub.Models;

namespace MecHub.ViewModel
{
    public class UsuarioCreateViewModel
    {
        [Required(ErrorMessage = "Informe seu nome.")]
        [StringLength(250)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu email.")]
        [StringLength(250)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha.")]
        [MinLength(10)]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "Confirme sua senha.")]
        [MinLength(10)]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
        public required string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Informe o telefone")]
        [Phone(ErrorMessage = "Informe um telefone válido")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    }
}

