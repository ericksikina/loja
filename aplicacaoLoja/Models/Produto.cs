using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace aplicacaoLoja.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50)]
        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Preço")]
        public decimal preco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Qtde Estoque")]
        public int qtdeEstoque { get; set; }

        public int categoriaID { get; set; }
        [ForeignKey("categoriaID")]
        [Display(Name = "Categoria")]
        public Categoria categoria { get; set; }

        public int fornecedorID { get; set; }
        [ForeignKey("fornecedorID")]
        [Display(Name = "Fornecedor")]
        public Fornecedor fornecedor { get; set; }

        public void atualizarEstoqueVenda(int qtde)
        {
            if (this.qtdeEstoque >= qtde)
                this.qtdeEstoque -= qtde;
            else
                throw new Exception("Estoque insuficiente!");
        }

        public void atualizarEstoqueCompra(int qtde)
        {
            this.qtdeEstoque += qtde;
        }

        public decimal calcularTotal(int qtde)
        {
            decimal total = qtde * this.preco;
            return total;
        }
    }
}
