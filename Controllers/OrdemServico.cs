using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;
using Microsoft.EntityFrameworkCore;

namespace MecHub.Controllers
{
    public class OrdemServicoController : Controller
    {
         private readonly AppDbContext _context;

         public OrdemServicoController(AppDbContext context)
        {
            _context = context;
        }

        // Listar servicos
        public IActionResult Index()
        {
            var ordensServico = _context.ordem_servico.ToList();

            if (ordensServico == null)
                return Content("Não existe OSs no BD");

            return Json(ordensServico);
        }

        // Listar servicos por ID
        public IActionResult Detalhes(int id)
        {
            var ordemServico = _context.ordem_servico
                .Include(o => o.Itens)
                .FirstOrDefault(o => o.Id == id);

            if (ordemServico == null)
                return Content("Servico não encontrado");

            return Json(ordemServico);
        }

        // Criar novos servicos
        public IActionResult Criar()
        {
            
            var ordemServico = new OrdemServico
            {
                MecanicoId = 9,
                ClienteId = 3,
                VeiculoId = 3,
                StatusId = 2,
                DataCriacao = DateTime.Now
            };

            _context.ordem_servico.Add(ordemServico);
            _context.SaveChanges();

            return Json(ordemServico);
        }

        // Deletar servico
        public IActionResult Deletar(int id)
        {
            var ordemServico = _context.ordem_servico.Find(id);

            if (ordemServico == null)
                return Content("Servico não encontrado");

            _context.ordem_servico.Remove(ordemServico);
            _context.SaveChanges();

            return Content("Servico deletado com sucesso!");
        }

        // Edtitar servico
        public IActionResult Editar(int id)
        {
            var ordemServico = _context.ordem_servico.Find(id);

            if (ordemServico == null)
                return Content("Servico não encontrado");

            ordemServico.DataFechamento = DateTime.Now;
            _context.SaveChanges();

            return Content("Ordem Atualizada");
        }

    }
}