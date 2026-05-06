using System.ComponentModel.DataAnnotations;
using MecHub.Models;
namespace MecHub.ViewModel
{
    public class VeiculoEditViewModel
    {
        public required string Placa { get; set; }

        public required string Cor { get; set; }

        public required int AnoFabricacao { get; set; }

        public required string Modelo { get; set; }

        public required string Marca { get; set; }

        public int ClienteId { get; set; }

        public StatusVeiculoEnum StatusAtual { get; set; }

        public string? ObservacaoStatus { get; set; }

        public DateTime DataAtualizacaoStatus { get; set; } = DateTime.Now;


    }
}
