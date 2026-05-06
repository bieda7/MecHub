using System.ComponentModel.DataAnnotations;
using MecHub.Models;

namespace MecHub.ViewModel
{
    public class VeiculoCreateViewModel
    {
        [Required(ErrorMessage = "CampoObrigatorio")]
        public required string Placa { get; set; }

        [Required(ErrorMessage = "CampoObrigatorio")]
        public required string Modelo { get; set; }

        [Required(ErrorMessage = "CampoObrigatorio")]
        public required string Marca { get; set; }

        [Required(ErrorMessage = "CampoObrigatorio")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "CampoObrigatorio")]

        public string Cor { get; set; }

        [Required(ErrorMessage = "CampoObrigatorio")]

        public int AnoFabricacao { get; set; }

        [Required(ErrorMessage = "CampoObrigatorio")]

        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "CampoObrigatorio")]

        public StatusVeiculoEnum StatusAtual { get; set; }

        [Required(ErrorMessage = "CampoObrigatorio")]

        public string? ObservacaoStatus { get; set; }
        

        [Required(ErrorMessage = "CampoObrigatorio")]

        public DateTime DataAtualizacaoStatus { get; set; }
    }
}
