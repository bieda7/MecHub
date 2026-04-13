using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;

namespace MecHub.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // Listar/Read usuarios
        public IActionResult Index()
        {
            var usuarios = _context.usuario.ToList();
            return Json(usuarios); // Apenas para teste
        }

        // Listar/Read usuario por ID
        public IActionResult Detalhe(int id)
        {
            var usuario = _context.usuario.Find(id);

            if (usuario == null)
                return Content("Usuário não encontrado");

            return Json(usuario);
        }

        // Inserir/Create usuarios
        public IActionResult Criar()
        {
            var usuario = new Usuario
            {
                Nome = "Teste2",
                Senha = "12345",
                TipoLogin = TipoLoginEnum.Local,
                Email = "teste2@email.com",
                DataCriacao = DateTime.Now
            };

            _context.usuario.Add(usuario);
            _context.SaveChanges();

            return Content("Usuário criado com sucesso!");
        }
        
        // Editar/Update usuario
        public IActionResult Editar(int id)
        {
            var usuario = _context.usuario.Find(id);

            if (usuario == null)
                return Content("Usuário não encontrado");

            usuario.Nome = "Nome Atualizado";
            _context.SaveChanges();

            return Content("Usuário Atualizado");
        }

        public IActionResult Deletar(int id)
        {
            var usuario = _context.usuario.Find(id);

            if (usuario == null)
                return Content("Usuário não encontrado");

            _context.usuario.Remove(usuario);
            _context.SaveChanges();

            return Content("Usuário deletado!");
            
        }
    }
}