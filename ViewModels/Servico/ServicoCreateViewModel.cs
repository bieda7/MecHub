using System.ComponentModel.DataAnnotations;

namespace MecHub.ViewModel
{
    public class ServicoCreateViewModel
    {
        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public required string Descricao { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public decimal Preco { get; set; }

        
        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public decimal Tipo { get; set; }

        
    }
}
