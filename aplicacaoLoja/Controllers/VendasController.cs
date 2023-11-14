using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aplicacaoLoja.Models;
using Microsoft.AspNetCore.Authorization;

namespace aplicacaoLoja.Controllers
{
    //[Authorize]
    public class VendasController : Controller
    {
        private readonly Contexto _context;

        public VendasController(Contexto context)
        {
            _context = context;
        }

        // GET: Vendas
        public async Task<IActionResult> Index(string busca, string tipo)
        {
            var contexto = _context.Vendas.Include(v => v.cliente).Include(v => v.funcionario).Include(v => v.produto);

            List<Venda> vendas = _context.Vendas.Include(v => v.cliente).Include(v => v.funcionario).Include(v => v.produto).ToList();

            if (busca != null)
            {
                if (tipo == "cliente")
                {
                    vendas = _context.Vendas.Include(v => v.cliente).Include(v => v.funcionario).Include(v => v.produto)
                        .Where(p => p.cliente.nome.Contains(busca))
                        .ToList();
                }

                else if (tipo == "funcionario")
                {
                    vendas = _context.Vendas.Include(v => v.cliente).Include(v => v.funcionario).Include(v => v.produto)
                        .Where(p => p.funcionario.nome.Contains(busca))
                        .ToList();
                }

                else if (tipo == "produto")
                {
                    vendas = _context.Vendas.Include(v => v.cliente).Include(v => v.funcionario).Include(v => v.produto)
                        .Where(p => p.produto.descricao.Contains(busca))
                        .ToList();
                }

            }

            ViewBag.Total = TotalizarVendas(vendas);

            return View(vendas);
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vendas == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                .Include(v => v.cliente)
                .Include(v => v.funcionario)
                .Include(v => v.produto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewData["clienteID"] = new SelectList(_context.Clientes, "id", "nome");
            ViewData["funcionarioID"] = new SelectList(_context.Funcionarios, "id", "nome");
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "descricao");
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,data,clienteID,funcionarioID,produtoID,quantidade,total")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                Produto produto = _context.Produtos.Find(venda.produtoID);

                if (produto != null)
                {
                    produto.atualizarEstoqueVenda(venda.quantidade);
                    venda.total = produto.calcularTotal(venda.quantidade);
                    _context.Add(venda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["clienteID"] = new SelectList(_context.Clientes, "id", "nome", venda.clienteID);
            ViewData["funcionarioID"] = new SelectList(_context.Funcionarios, "id", "nome", venda.funcionarioID);
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "descricao", venda.produtoID);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vendas == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            ViewData["clienteID"] = new SelectList(_context.Clientes, "id", "nome", venda.clienteID);
            ViewData["funcionarioID"] = new SelectList(_context.Funcionarios, "id", "nome", venda.funcionarioID);
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "descricao", venda.produtoID);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,data,clienteID,funcionarioID,produtoID,quantidade,total")] Venda venda)
        {
            if (id != venda.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Venda vendaExistente = _context.Vendas.Find(venda.id);

                    if (vendaExistente != null)
                    {
                        _context.Entry(vendaExistente).State = EntityState.Detached;

                        _context.Entry(venda).State = EntityState.Modified;

                        Produto produto = _context.Produtos.Find(venda.produtoID);
                        int qtdeVenda = vendaExistente.quantidade;
                        venda.total = produto.calcularTotal(venda.quantidade);

                        if (produto != null)
                        {
                            produto.atualizarEstoqueVenda(venda.quantidade - qtdeVenda);
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["clienteID"] = new SelectList(_context.Clientes, "id", "nome", venda.clienteID);
            ViewData["funcionarioID"] = new SelectList(_context.Funcionarios, "id", "nome", venda.funcionarioID);
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "descricao", venda.produtoID);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vendas == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                .Include(v => v.cliente)
                .Include(v => v.funcionario)
                .Include(v => v.produto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vendas == null)
            {
                return Problem("Entity set 'Contexto.Vendas'  is null.");
            }
            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private decimal TotalizarVendas(List<Venda> vendas)
        {
            decimal total = vendas.Sum(o => o.total);

            return total;
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.id == id);
        }
    }
}