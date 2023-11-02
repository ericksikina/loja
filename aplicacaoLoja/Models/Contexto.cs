using Microsoft.EntityFrameworkCore;

namespace aplicacaoLoja.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CompraProduto> CompraProdutos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
    }
}
