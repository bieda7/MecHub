using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;
using System.Windows.Markup;
using MecHub.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MecHub.Controllers
{
    public class MecanicoController : Controller
    {
        private readonly AppDbContext _context;

        public MecanicoController(AppDbContext context)
        {
            _context = context;
        }
        // Listar mecanicos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalClientes = _context.cliente.Count();
            ViewBag.TotalVeiculos = _context.veiculo.Count();
            ViewBag.TotalServicos = _context.servico.Count();
            ViewBag.TotalOrdens = _context.ordem_servico.Count();

            ViewBag.OrdensAbertas = _context.ordem_servico.Count(o => o.StatusOrdem == StatusOrdemEnum.Aberto);
            ViewBag.OrdensAndamento = _context.ordem_servico.Count(o => o.StatusOrdem == StatusOrdemEnum.Em_andamento);
            ViewBag.OrdensAguardando = _context.ordem_servico.Count(o => o.StatusOrdem == StatusOrdemEnum.AguardandoAprovacao);
            ViewBag.OrdensFechadas = _context.ordem_servico.Count(o => o.StatusOrdem == StatusOrdemEnum.Fechada);

            ViewBag.UltimasOrdens = _context.ordem_servico
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .OrderByDescending(o => o.DataCriacao)
                .Take(5)
                .ToList();

            return View();
        }

        //listar/Read Mecanicos por ID
        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            var mecanicos = _context.mecanico.Find(id);

            if (mecanicos == null)
                return NotFound();

            return View(mecanicos);
        }

        // Inserir/Create mecanicos
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(MecanicoCreateViewModel model)
        {

            var mecanico = new Mecanico
            {
                UsuarioId = 7,
                Telefone = "11999999999"
            };

            _context.mecanico.Add(mecanico);
            _context.SaveChanges();

            return Json(mecanico);
        }
        // Deletar mecanicos
        public IActionResult Deletar(int id)
        {
            var mecanico = _context.mecanico.Find(id);

            if (mecanico == null)
                return Content("Mecanico não encontrado");

            _context.mecanico.Remove(mecanico);
            _context.SaveChanges();

            return Content("Mecanico deletado!");
        }
        // Editar mecanicos
        public IActionResult Editar(int id)
        {
            var mecanico = _context.mecanico.Find(id);

            if (mecanico == null)
                return Content("Mecanico não encontrado");

            mecanico.Telefone = "11999998989";
            _context.SaveChanges();

            return Content("Mecanico Atualizado");
        }

    }
}