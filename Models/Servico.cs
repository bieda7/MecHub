namespace MecHub.Models
{
    public class Servico
    {
        public int Id { get; set; }

        public required string Descricao { get; set; }

        public required decimal Valor { get; set; }

        public required string Tipo { get; set; }

        public List<ItemOrdemServico> Itens { get; set; } = new();
    }
}