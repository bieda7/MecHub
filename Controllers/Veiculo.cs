using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;

namespace MecHub.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

         // Listar status ordens
        public IActionResult Index()
        {
            var veiculos = _context.veiculo.ToList();
            return Json(veiculos); // Apenas para teste
        }

        public IActionResult Criar()
        {
            var veiculo = new Veiculo
            {
                Marca = "Toyota",
                Modelo = "Etios",
                Placa = "ABC1234",
                Cor = "Branco",
                AnoFabricacao = 2015,
                DataCriacao = DateTime.Now,
                ClienteId = 3
            };

            _context.veiculo.Add(veiculo);
            _context.SaveChanges();

            return Content("Veiculo Criado");
        }

        // Deletar veiculo
        public IActionResult Deletar(int id)
        {
            var veiculo = _context.veiculo.Find(id);

            if (veiculo == null)
                return Content("Veiculo não encontrado");
            
            _context.veiculo.Remove(veiculo);
            _context.SaveChanges();

            return Content("Veiculo deletado");
        }

        // Editar veiculo
        public IActionResult Editar(int id)
        {
            var veiculo = _context.veiculo.Find(id);

            if (veiculo == null)
                return Content("Veiculo não encontrado");

            veiculo.Cor = "Roxo";
            _context.SaveChanges();

            return Content("Veiculo Atualizado");
        }

        // Buscar veiculo pelo ID
        public IActionResult Detalhes(int id)
        {
            var veiculo = _context.veiculo.Find(id);

            if (veiculo == null)
                return Content("Veiculo não encontrado");
            return Json(veiculo);
        }

    }
}