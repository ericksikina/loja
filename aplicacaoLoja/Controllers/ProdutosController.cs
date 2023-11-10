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
    public class ProdutosController : Controller
    {
        private readonly Contexto _context;

        public ProdutosController(Contexto context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index(string busca, string tipo)
        {
            var contexto = _context.Produtos.Include(p => p.categoria).Include(p => p.fornecedor);

            List<Produto> produto = _context.Produtos
                .Include(p => p.categoria)
                .Include(p => p.fornecedor)
                .ToList();

            if (busca != null)
            {
                if (tipo == "produto")
                {
                    produto = _context.Produtos
                        .Include(p => p.categoria)
                        .Include(p => p.fornecedor)
                        .Where(p => p.descricao.Contains(busca))
                        .ToList();
                }

                else if (tipo == "categoria")
                {
                    produto = _context.Produtos
                        .Include(p => p.categoria)
                        .Include(p => p.fornecedor)
                        .Where(p => p.categoria.descricao.Contains(busca))
                        .ToList();
                }

                else if (tipo == "categoria")
                {
                    produto = _context.Produtos
                        .Include(p => p.categoria)
                        .Include(p => p.fornecedor)
                        .Where(p => p.fornecedor.nome.Contains(busca))
                        .ToList();
                }
            }

            return View(produto);
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.categoria)
                .Include(p => p.fornecedor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["categoriaID"] = new SelectList(_context.Categorias, "id", "descricao");
            ViewData["fornecedorID"] = new SelectList(_context.Fornecedores, "id", "nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,descricao,preco,qtdeEstoque,categoriaID,fornecedorID")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaID"] = new SelectList(_context.Categorias, "id", "descricao", produto.categoriaID);
            ViewData["fornecedorID"] = new SelectList(_context.Fornecedores, "id", "nome", produto.fornecedorID);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["categoriaID"] = new SelectList(_context.Categorias, "id", "descricao", produto.categoriaID);
            ViewData["fornecedorID"] = new SelectList(_context.Fornecedores, "id", "nome", produto.fornecedorID);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,descricao,preco,qtdeEstoque,categoriaID,fornecedorID")] Produto produto)
        {
            if (id != produto.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.id))
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
            ViewData["categoriaID"] = new SelectList(_context.Categorias, "id", "descricao", produto.categoriaID);
            ViewData["fornecedorID"] = new SelectList(_context.Fornecedores, "id", "nome", produto.fornecedorID);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.categoria)
                .Include(p => p.fornecedor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'Contexto.Produtos'  is null.");
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Filtrar(string busca)
        {
            var produtos = _context.Produtos
                                     .Include(cat => cat.categoria)
                                     .Include(forn => forn.fornecedor)
                                     .Where(prod => prod.categoria.descricao.Contains(busca))
                                     .ToList();
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.id == id);
        }

    }
}