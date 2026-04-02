namespace MecHub.Models
{
    public class OrdemServico
    {
        public int Id { get; set; }

        public int MecanicoId { get; set; }

        public int ClienteId { get; set; }

        public int VeiculoId { get; set; }

        public int StatusId { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataFechamento { get; set; }

        // Relacionamentos
        public Mecanico? Mecanico { get; set; }
        public Cliente? Cliente { get; set; }
        public Veiculo? Veiculo { get; set; }
        public StatusOrdem? Status { get; set; }

        public List<ItemOrdemServico> Itens { get; set; } = new();
    }
}