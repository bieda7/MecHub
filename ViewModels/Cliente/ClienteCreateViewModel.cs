using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace MecHub.ViewModel
{
    public class ClienteCreateViewModel
    {
        [Column("id")]
        public int Id {get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        [Column("nome")]
        public required string Nome { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        [Phone]
        [Column("telefone")] 
        public required string Telefone { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        [Column("cpf")]
        public required string Cpf { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        [EmailAddress]
        [Column("email")]
        public required string Email {get; set ;}
    }
}
