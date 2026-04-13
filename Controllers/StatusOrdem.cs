using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;

namespace MecHub.Controllers
{
    public class StatusOrdemController : Controller
    {
         private readonly AppDbContext _context;

         public StatusOrdemController(AppDbContext context)
        {
            _context = context;
        }

        // Listar status ordens
        public IActionResult Index()
        {
            var statusOrdens = _context.status_ordem.ToList();
            return Json(statusOrdens); // Apenas para teste
        }

        // Criar status
        public IActionResult Criar()
        {

           var statusOrdem = new StatusOrdem
            {
                Descricao = "Fechada",
            };


            _context.status_ordem.Add(statusOrdem);
            _context.SaveChanges();

            return Json(statusOrdem);
        }

        // Deletar status ordem
        public IActionResult Deletar(int id)
        {
            var statusOrdem = _context.status_ordem.Find(id);

            if (statusOrdem == null)
                return Content("Status ordem");
            
            _context.status_ordem.Remove(statusOrdem);
            _context.SaveChanges();

            return Content("Status deletado");
        }


    }
}