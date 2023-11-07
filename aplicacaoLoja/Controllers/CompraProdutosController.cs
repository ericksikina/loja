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
    public class CompraProdutosController : Controller
    {
        private readonly Contexto _context;

        public CompraProdutosController(Contexto context)
        {
            _context = context;
        }

        // GET: CompraProdutos
        public async Task<IActionResult> Index()
        {
            var contexto = _context.CompraProdutos.Include(c => c.produto);
            return View(await contexto.ToListAsync());
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
                return RedirectToAction("Index", "Produtos");
            }
            ViewData["produtoID"] = new SelectList(_context.Produtos, "id", "descricao", compraProduto.produtoID);
            return View(compraProduto);
        }


    }
}
