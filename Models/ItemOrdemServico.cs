namespace MecHub.Models
{
    public class ItemOrdemServico
    {
        public int Id { get; set; }

        public int OrdemServicoId { get; set; }

        public int ServicoId { get; set; }

        public int Quantidade { get; set; }

        // Relacionamentos
        public OrdemServico? OrdemServico { get; set; } 
        public Servico? Servico { get; set; }
    }
}