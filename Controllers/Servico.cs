using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;
using MecHub.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MecHub.Controllers
{

    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class ServicoController : Controller
    {
        private readonly AppDbContext _context;

        private int ObterMecanicoId()
        {
            var mecanicoId = User.FindFirstValue("MecanicoId");

            if (string.IsNullOrWhiteSpace(mecanicoId))
                throw new UnauthorizedAccessException("MecanicoId não encontrado na sessão.");

            return int.Parse(mecanicoId);
        }

        public ServicoController(AppDbContext context)
        {
            _context = context;
        }

        // Listar servicos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var mecanicoId = ObterMecanicoId();

            var servicos = await _context.servico
                .Where(s => s.MecanicoId == mecanicoId)
                .Select(s => new ServicoListViewModel
                {
                    Id = s.Id,
                    Descricao = s.Descricao,
                    Valor = s.Valor,
                    Tipo = s.Tipo
                })
                .ToListAsync();

            return View(servicos);
        }
        // Listar servicos por ID
        [HttpGet]
        public async Task<IActionResult> Detalhe(int id)
        {

            var mecanicoId = ObterMecanicoId();

            var servico = await _context.servico
                .FirstOrDefaultAsync(s => s.Id == id && s.MecanicoId == mecanicoId);

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

            var mecanicoId = ObterMecanicoId();

            var servico = new Servico
            {
                Descricao = model.Descricao,
                Valor = model.Valor,
                Tipo = model.Tipo,
                MecanicoId = mecanicoId
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
        public async Task<IActionResult> Deletar(int id)
        {
            var mecanicoId = ObterMecanicoId();

            var servico = await _context.servico
                .FirstOrDefaultAsync(s => s.Id == id && s.MecanicoId == mecanicoId);

            if (servico == null)
                return NotFound();

            return View(servico);
        }

        // Deletar servico
        [HttpPost]
        [ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            var mecanicoId = ObterMecanicoId();

            var servico = await _context.servico
                .FirstOrDefaultAsync(s => s.Id == id && s.MecanicoId == mecanicoId);

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
        public async Task<IActionResult> Editar(int id, ServicoEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var mecanicoId = ObterMecanicoId();

            var servico = await _context.servico
                .FirstOrDefaultAsync(s => s.Id == id && s.MecanicoId == mecanicoId);

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