using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;
using System.Windows.Markup;

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
        public IActionResult Index()
        {
            var mecanicos = _context.mecanico.ToList();
            return Json(mecanicos); // Apenas para teste
        }

        //listar/Read Mecanicos por ID
        public IActionResult Detalhe(int id)
        {
            var mecanicos = _context.mecanico.Find(id);

            if (mecanicos == null)
                return Content("Mecanico não encontrado");

            return Json(mecanicos);
        }

        // Inserir/Create mecanicos
        public IActionResult Criar()
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