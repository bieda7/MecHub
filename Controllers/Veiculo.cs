using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;
using MecHub.ViewModel;

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
        [HttpGet]
        public IActionResult AtualizarStatus(int id)
        {
            var veiculo = _context.veiculo.FirstOrDefault(v => v.Id == id);

            if (veiculo == null)
                return NotFound();

            var model = new VeiculoAtualizarStatusViewModel
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                StatusAtual = veiculo.StatusAtual,
                ObservacaoStatus = veiculo.ObservacaoStatus
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AtualizarStatus(VeiculoAtualizarStatusViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var veiculo = _context.veiculo.FirstOrDefault(v => v.Id == model.Id);

            if (veiculo == null)
                return NotFound();

            veiculo.StatusAtual = model.StatusAtual;
            veiculo.ObservacaoStatus = model.ObservacaoStatus;
            veiculo.DataAtualizacaoStatus = DateTime.Now;

            _context.SaveChanges();

            TempData["Sucesso"] = "Status do veículo atualizado com sucesso.";

            return RedirectToAction("Index");
        }
    }
}