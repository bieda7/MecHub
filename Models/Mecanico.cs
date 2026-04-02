namespace MecHub.Models
{
    public class Mecanico
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public required string Telefone { get; set; }

        // Relacionamentos
        public Usuario? Usuario { get; set; }
        public List<OrdemServico> OrdensServico { get; set; } = new();
    }
}