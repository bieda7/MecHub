namespace MecHub.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Telefone { get; set; }
        public required string Cpf { get; set; }
        public List<Veiculo> Veiculos { get; set; } = new();
    }
}