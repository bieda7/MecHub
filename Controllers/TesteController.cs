using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;

namespace MecHub.Controllers
{
    public class TesteController : Controller
    {
        private readonly AppDbContext _context;

        public TesteController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return Content("API funcionando 🚀");
        }

        public IActionResult TestarConexao()
        {
            try
            {
                var podeConectar = _context.Database.CanConnect();

                if (podeConectar)
                    return Content("✅ Conexão com banco OK");
                else
                    return Content("❌ Falha na conexão");
            }
            catch (Exception ex)
            {
                return Content($"Erro: {ex.Message}");
            }
        }
    }
}