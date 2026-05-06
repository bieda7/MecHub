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
        public decimal Valor { get; set; }

        
        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public required string  Tipo { get; set; }

        
    }
}
