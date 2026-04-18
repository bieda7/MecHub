using System.ComponentModel.DataAnnotations;

namespace MecHub.ViewModel
{
    public class ServicoEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public required string Descricao { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public decimal Valor { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public decimal Tipo { get; set; }


    }
}
