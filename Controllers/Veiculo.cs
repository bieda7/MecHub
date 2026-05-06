using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var veiculos = await _context.veiculo
                .Include(v => v.Cliente)
                .OrderBy(v => v.Placa)
                .Select(v => new VeiculoListViewModel
                {
                    Id = v.Id,
                    Placa = v.Placa,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Cor = v.Cor,
                    AnoFabricacao = v.AnoFabricacao,
                    DataCriacao = v.DataCriacao,
                    StatusAtual = v.StatusAtual,
                    ClienteNome = v.Cliente != null ? v.Cliente.Nome : "Sem cliente"
                })
                .ToListAsync();

            return View(veiculos);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(VeiculoCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var placaExiste = await _context.veiculo
                .AnyAsync(v => v.Placa == model.Placa);

            if (placaExiste)
            {
                ModelState.AddModelError("Placa", "Já existe um veículo cadastrado com esta placa.");
                return View(model);
            }

            var clienteExiste = await _context.cliente
                .AnyAsync(c => c.Id == model.ClienteId);

            if (!clienteExiste)
            {
                ModelState.AddModelError("ClienteId", "Cliente informado não existe.");
                return View(model);
            }

            var veiculo = new Veiculo
            {
                Placa = model.Placa,
                Modelo = model.Modelo,
                Marca = model.Marca,
                ClienteId = model.ClienteId,
                Cor = model.Cor,
                AnoFabricacao = model.AnoFabricacao
            };

            _context.veiculo.Add(veiculo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var veiculo = await _context.veiculo
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (veiculo == null)
                return NotFound();

            return View(veiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var veiculo = await _context.veiculo.FindAsync(id);

            if (veiculo == null)
                return NotFound();

            return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Veiculo model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var veiculo = await _context.veiculo.FindAsync(model.Id);

            if (veiculo == null)
                return NotFound();

            veiculo.Placa = model.Placa;
            veiculo.Marca = model.Marca;
            veiculo.Modelo = model.Modelo;
            veiculo.Cor = model.Cor;
            veiculo.AnoFabricacao = model.AnoFabricacao;
            veiculo.ClienteId = model.ClienteId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(int id)
        {
            var veiculo = await _context.veiculo
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (veiculo == null)
                return NotFound();

            return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            var veiculo = await _context.veiculo.FindAsync(id);

            if (veiculo == null)
                return NotFound();

            _context.veiculo.Remove(veiculo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarStatus(int id)
        {
            var veiculo = await _context.veiculo.FirstOrDefaultAsync(v => v.Id == id);

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
        public async Task<IActionResult> AtualizarStatus(VeiculoAtualizarStatusViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var veiculo = await _context.veiculo.FirstOrDefaultAsync(v => v.Id == model.Id);

            if (veiculo == null)
                return NotFound();

            veiculo.StatusAtual = model.StatusAtual;
            veiculo.ObservacaoStatus = model.ObservacaoStatus;
            veiculo.DataAtualizacaoStatus = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Status do veículo atualizado com sucesso.";

            return RedirectToAction(nameof(Index));
        }
    }
}