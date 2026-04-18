using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;
using System.Windows.Markup;
using MecHub.ViewModel;

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
        public IActionResult Index()
        {
            var mecanicos = _context.mecanico.ToList();
            return View(mecanicos);
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
        public IActionResult Criar(MecanicoViewModel model)
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