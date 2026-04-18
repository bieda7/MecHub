namespace MecHub.ViewModel
{
    public class VeiculoListViewModel
    {
        public int Id { get; set; }

        public required string Placa { get; set; }

        public required string Modelo { get; set; }

        public required string Marca { get; set; }

        public required string ClienteNome { get; set; }
    }
}
