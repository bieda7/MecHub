using System.ComponentModel.DataAnnotations.Schema;

namespace MecHub.Models
{
    public class StatusOrdem
    {
        public int Id { get; set; }

        public required string Descricao { get; set; }

        public List<OrdemServico> OrdensServico { get; set; } = new();
    }
}