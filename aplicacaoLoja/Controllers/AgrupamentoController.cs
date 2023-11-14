using aplicacaoLoja.Models;
using aplicacaoLoja.Models.Consulta.Agrupamento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aplicacaoLoja.Controllers
{
    public class AgrupamentoController : Controller
    {

        private readonly Contexto contexto;

        public AgrupamentoController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult AgrVendas()
        {
            IEnumerable<LstVendas> lstVendas =
                from item in contexto.Vendas
                .Include(o => o.cliente)
                .Include(o => o.funcionario)
                .Include(o => o.produto)
                .OrderBy(o => o.clienteID)
                .ToList()
                select new LstVendas
                {
                    data = item.data,
                    clienteId = item.clienteID,
                    cliente = item.cliente.nome,
                    funcionario = item.funcionario.nome,
                    produto = item.produto.descricao,
                    quantidade = item.quantidade,
                    total = item.total
                };

            IEnumerable<AgrVendas> agrVendas =
                from linha in lstVendas
                .ToList()
                group linha by new { linha.clienteId }
                into grupo
                orderby grupo.Key.clienteId
                select new AgrVendas
                {
                    clienteId = grupo.Key.clienteId,
                    quantidade = grupo.Sum(o => o.quantidade),
                    total = grupo.Sum(o => o.total)
                };

            return View(agrVendas);
        }
    }
}
