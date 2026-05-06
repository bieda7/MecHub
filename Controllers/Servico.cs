using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;
using MecHub.ViewModel;

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
        [HttpGet]
        public IActionResult Index()
        {
            var servicos = _context.servico
                .Select(s => new ServicoListViewModel
                {
                    Id = s.Id,
                    Descricao = s.Descricao,
                    Valor = s.Valor,
                    Tipo = s.Tipo
                })
                .ToList();

            return View(servicos);
        }
        // Listar servicos por ID
        [HttpGet]
        public IActionResult Detalhe(int id)
        {

            var servico = _context.servico.Find(id);

            if (servico == null)
                return Content("Servico não encontrado");

            return View(servico);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // Criar novos servicos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(ServicoCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var servico = new Servico
            {
                Descricao = model.Descricao,
                Valor = model.Valor,
                Tipo = model.Tipo
            };

            try
            {
                _context.servico.Add(servico);
                _context.SaveChanges();

                return View(servico);
            }
            catch (Exception)
            {
                // Log futuramente (ILogger)
                ModelState.AddModelError("", "Não foi possível salvar o serviço. Tente novamente.");

                return View(model);
            }

        }


        [HttpGet]
        public IActionResult Deletar(int id)
        {
            var servico = _context.servico.Find(id);

            if (servico == null)
                return NotFound();

            return View(servico);
        }

        // Deletar servico
        [HttpPost]
        [ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletarConfirmado(int id)
        {
            var servico = _context.servico.Find(id);

            if (servico == null)
                return NotFound();

            try
            {
                _context.servico.Remove(servico);
                _context.SaveChanges();

                return View("Index", "Mecanico");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erro ao tentar deletar o cliente.");
                return View(servico);
            }
        }

        // Edtitar servico
        [HttpPost]
        public IActionResult Editar(int id, ServicoEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var servico = _context.servico.Find(id);

            if (id != model.Id)
                return BadRequest();

            if (servico == null)
                return NotFound();

            servico.Descricao = model.Descricao;
            servico.Valor = model.Valor;
            servico.Tipo = model.Tipo;

            try
            {
                _context.SaveChanges();
                return View("Index", "Mecanico");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erro ao tentar editar dados do servico.");
                return View(model);
            }
        }

    }
}