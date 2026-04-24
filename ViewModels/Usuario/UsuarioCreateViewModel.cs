using System.ComponentModel.DataAnnotations;
using MecHub.Models;

namespace MecHub.ViewModel
{
    public class UsuarioCreateViewModel
    {
        [Required(
            ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource)
        )]
        public required string Nome { get; set; }

        [Required(
            ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource)
        )]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(
            ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource)
        )]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

        [Required(
            ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource)
        )]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
        public required string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Informe o telefone")]
        [Phone(ErrorMessage = "Informe um telefone válido")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    }
}

