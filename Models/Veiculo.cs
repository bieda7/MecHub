namespace MecHub.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        public required string Marca { get; set; }

        public required string Modelo { get; set; }

        public required string Placa { get; set; }

        public required string Cor { get; set; }

        public required int AnoFabricacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public int ClienteId { get; set; }

        // Relacionamentos
        public Cliente? Cliente { get; set; }
        public List<OrdemServico> OrdensServico { get; set; } = new();
    }
}