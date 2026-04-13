using Microsoft.AspNetCore.Mvc;
using MecHub.Data;
using MecHub.Models;

namespace MecHub.Controllers
{
    public class ItemOrdemServicoController : Controller
    {
        private readonly AppDbContext _context;

        public ItemOrdemServicoController(AppDbContext context)
        {
            _context = context;
        }

        // Listar todos os itens
        public IActionResult Index()
        {
            var itens = _context.item_ordem_servico.ToList();
            return Json(itens);
        }

        // Buscar item por ID
        public IActionResult Detalhes(int id)
        {
            var item = _context.item_ordem_servico.Find(id);

            if (item == null)
                return Content("Item não encontrado");

            return Json(item);
        }

        // Criar item (ligando a uma OS já existente)
        public IActionResult Criar()
        {
            var item = new ItemOrdemServico
            {
                OrdemServicoId = 3, // ⚠️ precisa existir
                ServicoId = 1,      // ⚠️ precisa existir
                Quantidade = 2
            };

            _context.item_ordem_servico.Add(item);
            _context.SaveChanges();

            return Json(item);
        }

        // Deletar item
        public IActionResult Deletar(int id)
        {
            var item = _context.item_ordem_servico.Find(id);

            if (item == null)
                return Content("Item não encontrado");

            _context.item_ordem_servico.Remove(item);
            _context.SaveChanges();

            return Content("Item deletado com sucesso!");
        }

        // Editar item
        public IActionResult Editar(int id)
        {
            var item = _context.item_ordem_servico.Find(id);

            if (item == null)
                return Content("Item não encontrado");

            item.Quantidade += 1;
            _context.SaveChanges();

            return Content("Item atualizado!");
        }

        public IActionResult CriarComItens()
        {
            var ordemServico = new OrdemServico
            {
                MecanicoId = 9,
                ClienteId = 3,
                VeiculoId = 3,
                StatusId = 2,
                DataCriacao = DateTime.Now,

                // 👇 Aqui está o segredo
                Itens = new List<ItemOrdemServico>
                {
                    new ItemOrdemServico
                    {
                        ServicoId = 1,
                        Quantidade = 2
                    },
                    new ItemOrdemServico
                    {
                        ServicoId = 2,
                        Quantidade = 1
                    }
                }
            };

            _context.ordem_servico.Add(ordemServico);
            _context.SaveChanges();

            return Json(ordemServico);
        }
    }
}