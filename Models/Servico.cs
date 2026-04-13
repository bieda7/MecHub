using System.ComponentModel.DataAnnotations.Schema;

namespace MecHub.Models
{
    public class Servico
    {

        [Column("id")]
        public int Id { get; set; }

        [Column("descricao")]
        public required string Descricao { get; set; }

        [Column("valor")]
        public required decimal Valor { get; set; }

        [Column("tipo")]
        public required string Tipo { get; set; }

        public List<ItemOrdemServico> Itens { get; set; } = new();
    }
}