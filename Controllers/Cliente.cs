using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;

namespace MecHub.Controllers
{
    
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        // Listar/Read clientes
        public IActionResult Index()
        {
            var clientes = _context.cliente.ToList();
            return Json(clientes); // Apenas para teste
        }

        public IActionResult Detalhe(int id)
        {
            var cliente = _context.cliente.Find(id);

            if (cliente == null)
                return Content("Cliente não encontrado");

            return Json(cliente);
        }



        // Criar clientes
        public IActionResult Criar()
        {
            var cliente = new Cliente
            {
                Nome = "Guilherme",
                Telefone = "11999999999",
                Cpf = "134.526.859-60"
            };

            _context.cliente.Add(cliente);
            _context.SaveChanges();

            return Content("Cliente criado com sucesso!");
        }

        // Editar clientes
        public IActionResult Editar(int id)
        {
            var cliente = _context.cliente.Find(id);

            if (cliente == null)
                return Content("Usuário não encontrado");

            cliente.Nome = "Nome Atualizado";
            _context.SaveChanges();

            return Content("Cliente Atualizado");
        }

        // Deletar clientes
        public IActionResult Deletar(int id)
        {
            var cliente = _context.cliente.Find(id);

            if (cliente == null)
                return Content("Cliente não encontrado");

            _context.cliente.Remove(cliente);
            _context.SaveChanges();

            return Content("Cliente deletado!");
            
        }


    }
}