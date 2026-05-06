using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MecHub.ViewModel;

namespace MecHub.Controllers
{
    
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // Listar clientes
        public IActionResult Index()
        {
            var clientes = _context.cliente.ToList();
            return View(clientes);    
        }

        [HttpGet]
        public IActionResult Detalhe(int id)
        {
            var cliente = _context.cliente.Find(id);

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

            var cliente = new Cliente
            {
                Nome = model.Nome,
                Telefone = model.Telefone,
                Cpf = model.Cpf,
                Email = model.Email
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
        public IActionResult Editar(int id)
        {
            var cliente = _context.cliente.Find(id);

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
        public IActionResult Editar(int id, ClienteEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var cliente = _context.cliente.Find(id);

            if (id != model.Id)
                return BadRequest();

            if (cliente == null)
                return NotFound();

            cliente.Nome = model.Nome;
            cliente.Telefone = model.Telefone;
            cliente.Cpf = model.Cpf;

            try
            {
                _context.SaveChanges();    
                return RedirectToAction("Index", "Mecanico");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erro ao tentar editar dados do cliente.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Deletar(int id)
        {
            var cliente = _context.cliente.Find(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }
        
        [HttpPost]
        [ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        // Deletar clientes
        public IActionResult DeletarConfirmado(int id)
        {
            var cliente = _context.cliente.Find(id);

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