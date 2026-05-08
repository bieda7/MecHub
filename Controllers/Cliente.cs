using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MecHub.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MecHub.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;

        private int ObterMecanicoId()
        {
            var mecanicoId = User.FindFirstValue("MecanicoId");

            if (string.IsNullOrWhiteSpace(mecanicoId))
                throw new UnauthorizedAccessException("MecanicoId não encontrado na sessão.");

            return int.Parse(mecanicoId);
        }

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // Listar clientes
        public async Task<IActionResult> Index()
        {
            var mecanicoId = ObterMecanicoId();

            var clientes = await _context.cliente
                .Where(c => c.MecanicoId == mecanicoId)
                .ToListAsync();

            return View(clientes);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhe(int id)
        {
            var mecanicoId = ObterMecanicoId();

            var cliente = await _context.cliente
                .FirstOrDefaultAsync(c => c.Id == id && c.MecanicoId == mecanicoId);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }


        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        // Criar clientes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(ClienteCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var mecanicoId = ObterMecanicoId();

            var cliente = new Cliente
            {
                Nome = model.Nome,
                Telefone = model.Telefone,
                Cpf = model.Cpf,
                Email = model.Email,
                MecanicoId = mecanicoId
            };

            try
            {
                _context.cliente.Add(cliente);
                _context.SaveChanges();

                return RedirectToAction("Index", "Mecanico");
            }
            catch (Exception)
            {
                // Log futuramente (ILogger)
                ModelState.AddModelError("", "Não foi possível salvar o cliente. Tente novamente.");

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var mecanicoId = ObterMecanicoId();

            var cliente = await _context.cliente
                .FirstOrDefaultAsync(c => c.Id == id && c.MecanicoId == mecanicoId);

            if (cliente == null)
                return NotFound();

            var model = new ClienteEditViewModel
            {
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Cpf = cliente.Cpf,
                Email = cliente.Email

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Editar clientes
        public async Task<IActionResult> Editar(int id, ClienteEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var mecanicoId = ObterMecanicoId();

            var cliente = await _context.cliente
                .FirstOrDefaultAsync(c => c.Id == id && c.MecanicoId == mecanicoId);

            if (id != model.Id)
                return BadRequest();

            if (cliente == null)
                return NotFound();

            cliente.Nome = model.Nome;
            cliente.Telefone = model.Telefone;
            cliente.Cpf = model.Cpf;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Mecanico");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erro ao tentar editar dados do cliente.");
                return View(model);
            }
        }

        [HttpGet]
       public async Task<IActionResult>  Deletar(int id)
        {
            var mecanicoId = ObterMecanicoId();

            var cliente = await _context.cliente
                .FirstOrDefaultAsync(c => c.Id == id && c.MecanicoId == mecanicoId);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        [ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        // Deletar clientes
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            var mecanicoId = ObterMecanicoId();

            var cliente = await _context.cliente
                .FirstOrDefaultAsync(c => c.Id == id && c.MecanicoId == mecanicoId);

            if (cliente == null)
                return NotFound();

            try
            {
                _context.cliente.Remove(cliente);
                _context.SaveChanges();

                return RedirectToAction("Index", "Mecanico");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erro ao tentar deletar o cliente.");
                return View(cliente);
            }


        }

    }
}

