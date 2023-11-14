﻿using aplicacaoLoja.Models.Consulta.Agrupamento;
using aplicacaoLoja.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aplicacaoLoja.Extra;
using aplicacaoLoja.Models.Consulta.Pivot;
using System.Data;

namespace aplicacaoLoja.Controllers
{
    public class PivotController : Controller
    {
        private readonly Contexto contexto;

        public PivotController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult PivotVendas(int ano)
        {
            IEnumerable<LstVendas> lstVendas =
                from item in contexto.Vendas
                .Include(o => o.cliente)
                .Include(o => o.funcionario)
                .Include (o => o.produto)
                .OrderBy(o => o.clienteID)
                .Where(o => o.data.Year == ano)
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

            IEnumerable<VendasMes> agrVendasMes =
                from linha in lstVendas
                .ToList()
                group linha by new { linha.clienteId, linha.data.Month}
                into grupo
                orderby grupo.Key.clienteId
                select new VendasMes
                {
                    cliente = grupo.Key.clienteId,
                    mes = grupo.Key.Month,
                    total = grupo.Sum(o => o.total)
                };

            var PivotTableInsArea = agrVendasMes.ToList().ToPivotTable(
                pivo => pivo.mes,
                pivo => pivo.cliente,
                pivo => pivo.Any() ? pivo.Sum(x => x.total) : 0);

            List<ConsultaPivot> lista = new List<ConsultaPivot>();
            lista = (from DataRow coluna in PivotTableInsArea.Rows
                     select new ConsultaPivot()
                     {
                         cliente = Convert.ToInt32(coluna[0]),
                         mes1 = Convert.ToSingle(coluna[1]),
                         mes2 = Convert.ToSingle(coluna[2]),
                         mes3 = Convert.ToSingle(coluna[3]),
                         mes4 = Convert.ToSingle(coluna[4]),
                         mes5 = Convert.ToSingle(coluna[5]),
                         mes6 = Convert.ToSingle(coluna[6]),
                         mes7 = Convert.ToSingle(coluna[7]),
                         mes8 = Convert.ToSingle(coluna[8]),
                         mes9 = Convert.ToSingle(coluna[9]),
                         mes10 = Convert.ToSingle(coluna[10]),
                         mes11 = Convert.ToSingle(coluna[11]),
                         mes12 = Convert.ToSingle(coluna[12]),
                     }).ToList();

            return View(lista);
        }
    }
}