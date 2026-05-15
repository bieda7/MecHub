using System.ComponentModel.DataAnnotations;

namespace MecHub.ViewModel;

public class ResetarSenhaViewModel
{
    public string Email { get; set; }
    
    public string Token { get; set; }

    [Required(ErrorMessage = "Informe a nova senha.")]
    [DataType(DataType.Password)]
    public string NovaSenha { get; set; }

    [Required(ErrorMessage = "Confirme a nova senha.")]
    [DataType(DataType.Password)]
    [Compare("NovaSenha", ErrorMessage = "As senhas não conferem.")]
    public string ConfirmarSenha { get; set; }
}