using System.ComponentModel.DataAnnotations;

namespace MecHub.ViewModel
{
    public class ItemOrdemServicoCreateViewModel
    {
        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public int OrdemServicoId { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public int ServicoId { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public int Quantidade { get; set; }

        public required List<ItemOrdemServicoViewModel> Itens { get; set; }
    }
}
