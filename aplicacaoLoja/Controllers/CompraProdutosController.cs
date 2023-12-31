﻿using System;
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
    [Authorize]
    public class CompraProdutosController : Controller
    {
        private readonly Contexto _context;

        public CompraProdutosController(Contexto context)
        {
            _context = context;
        }

        // GET: CompraProdutos
        public async Task<IActionResult> Index(string busca)
        {
            List<CompraProduto> compProduto = _context.CompraProdutos.Include(c => c.produto).ToList();

            if (busca != null)
            {
                compProduto = _context.CompraProdutos.Include(v => v.produto)
                        .Where(p => p.produto.descricao.Contains(busca))
                        .ToList();
            }

            var contexto = _context.CompraProdutos.Include(c => c.produto);
            return View(compProduto);
        }

        // GET: CompraProdutos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CompraProdutos == null)
            {
                return NotFound();
            }

            var compraProduto = await _context.CompraProdutos
                .Include(c => c.produto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (compraProduto == null)
            {
                return NotFound();
            }

            return View(compraProduto);
        }

        // GET: CompraProdutos/Create
        public IActionResult Create()
        {
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "descricao");
            return View();
        }

        // POST: CompraProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,produtoID,qtde")] CompraProduto compraProduto)
        {
            if (ModelState.IsValid)
            {
                Produto produto = _context.Produtos.Find(compraProduto.produtoID);
                produto.qtdeEstoque = produto.qtdeEstoque + compraProduto.qtde;
                _context.Update(produto);

                _context.Add(compraProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "CompraProdutos");
            }
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "descricao", compraProduto.produtoID);
            return View("Index");
        }

        // GET: CompraProdutos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CompraProdutos == null)
            {
                return NotFound();
            }

            var compraProduto = await _context.CompraProdutos.FindAsync(id);
            if (compraProduto == null)
            {
                return NotFound();
            }
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "descricao", compraProduto.produtoID);
            return View(compraProduto);
        }

        // POST: CompraProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,produtoID,qtde")] CompraProduto compraProduto)
        {
            if (id != compraProduto.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalCompraProduto = await _context.CompraProdutos.AsNoTracking().FirstOrDefaultAsync(cp => cp.id == compraProduto.id);

                    _context.Update(compraProduto);

                    Produto produto = _context.Produtos.Find(compraProduto.produtoID);
                    produto.qtdeEstoque += compraProduto.qtde - originalCompraProduto.qtde;
                    _context.Update(produto);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraProdutoExists(compraProduto.id))
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
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "descricao", compraProduto.produtoID);
            return View(compraProduto);
        }

        // GET: CompraProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompraProdutos == null)
            {
                return NotFound();
            }

            var compraProduto = await _context.CompraProdutos
                .Include(c => c.produto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (compraProduto == null)
            {
                return NotFound();
            }

            return View(compraProduto);
        }

        // POST: CompraProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CompraProdutos == null)
            {
                return Problem("Entity set 'Contexto.CompraProdutos'  is null.");
            }
            var compraProduto = await _context.CompraProdutos.FindAsync(id);
            if (compraProduto != null)
            {
                _context.CompraProdutos.Remove(compraProduto);

                Produto produto = _context.Produtos.Find(compraProduto.produtoID);
                produto.qtdeEstoque -= compraProduto.qtde;
                _context.Update(produto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraProdutoExists(int id)
        {
          return _context.CompraProdutos.Any(e => e.id == id);
        }
    }
}
