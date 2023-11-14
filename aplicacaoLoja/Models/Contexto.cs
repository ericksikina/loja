using Microsoft.EntityFrameworkCore;
using aplicacaoLoja.Models.Consulta.Agrupamento;
using aplicacaoLoja.Models.Consulta.Pivot;

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
        public DbSet<aplicacaoLoja.Models.Consulta.Agrupamento.AgrVendas> AgrVendas { get; set; }
        public DbSet<aplicacaoLoja.Models.Consulta.Pivot.ConsultaPivot> ConsultaPivot { get; set; }
    }
}
