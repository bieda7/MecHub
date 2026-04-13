using System.ComponentModel.DataAnnotations.Schema;

namespace MecHub.Models
{
    public class Veiculo
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("marca")]
        public required string Marca { get; set; }

        [Column("modelo")]
        public required string Modelo { get; set; }

        [Column("placa")]
        public required string Placa { get; set; }

        [Column("cor")]
        public required string Cor { get; set; }

        [Column("ano_fabricacao")]
        public required int AnoFabricacao { get; set; }

        [Column("data_criacao")]
        public DateTime DataCriacao { get; set; }

        [Column("cliente_id")]
        public int ClienteId { get; set; }

        // Relacionamentos
        public Cliente? Cliente { get; set; }
        public List<OrdemServico> OrdensServico { get; set; } = new();
    }
}