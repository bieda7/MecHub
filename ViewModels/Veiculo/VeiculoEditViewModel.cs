using System.ComponentModel.DataAnnotations;

namespace MecHub.ViewModel
{
    public class VeiculoEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "CampoObrigatorio",
            ErrorMessageResourceType = typeof(SharedResource))]
        public required string Placa { get; set; }


        public required string Modelo { get; set; }

        public required string Marca { get; set; }

        public int ClienteId { get; set; }
    }
}
