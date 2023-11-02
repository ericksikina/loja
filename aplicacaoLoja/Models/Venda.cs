using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace aplicacaoLoja.Models
{
    [Table("Vendas")]
    public class Venda
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.DateTime)]
        public DateTime data { get; set; }

        [Display(Name = "Cliente")]
        public int clienteID { get; set; }
        [ForeignKey("clienteID")]
        public Cliente cliente { get; set; }

        [Display(Name = "Funcionário")]
        public int funcionarioID { get; set; }
        [ForeignKey("funcionarioID")]
        public Funcionario funcionario { get; set; }

        [Display(Name = "Produto")]
        public int produtoID { get; set; }
        [ForeignKey("produtoID")]
        public Produto produto { get; set; }

        [Display(Name = "Quantidade")]
        public int quantidade { get; set; }

        [Display(Name = "Total")]
        public decimal total { get; set; }
    }
}
