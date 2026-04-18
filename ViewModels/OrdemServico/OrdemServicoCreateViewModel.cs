using System.ComponentModel.DataAnnotations;

namespace MecHub.ViewModel
{
    public class OrdemServicoCreateViewModel
    {

        public int MecanicoId {get; set; }
        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public int ClienteId { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public int VeiculoId { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public int StatusId {get; set; }

        public List<ItemOrdemServicoCreateViewModel> Itens { get; set; } = new();
    }
}
