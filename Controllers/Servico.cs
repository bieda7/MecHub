using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;

namespace MecHub.Controllers
{
    public class ServicoController : Controller
    {
         private readonly AppDbContext _context;

         public ServicoController(AppDbContext context)
        {
            _context = context;
        }

        // Listar servicos
        public IActionResult Index()
        {
            var servicos = _context.servico.ToList();
            return Json(servicos); // Apenas para teste
        }

        // Listar servicos por ID
        public IActionResult Detalhes(int id)
        {
            
            var servico = _context.servico.Find(id);

            if (servico == null)
                return Content("Servico não encontrado");

            return Json(servico);
        }

        // Criar novos servicos
        public IActionResult Criar()
        {
            
            var servico = new Servico
            {
                Descricao = "Troca de embreagem",
                Valor = 1000,
                Tipo = "Manutenção"
            };

            _context.servico.Add(servico);
            _context.SaveChanges();

            return Json(servico);
        }

        // Deletar servico
        public IActionResult Deletar(int id)
        {
            var servico = _context.servico.Find(id);

            if (servico == null)
                return Content("Servico não encontrado");

            _context.servico.Remove(servico);
            _context.SaveChanges();

            return Content("Servico deletado com sucesso!");
        }

        // Edtitar servico
        public IActionResult Editar(int id)
        {
            var servico = _context.servico.Find(id);

            if (servico == null)
                return Content("Servico não encontrado");

            servico.Descricao = "Descrição atualizada";
            _context.SaveChanges();

            return Content("Servico Atualizado");
        }

    }
}