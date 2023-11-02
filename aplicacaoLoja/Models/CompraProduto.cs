using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace aplicacaoLoja.Models
{
    [Table("CompraProdutos")]
    public class CompraProduto
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        public int produtoID { get; set; }
        [ForeignKey("produtoID")]
        [Display(Name = "Produto")]
        public Produto produto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Quatidade")]
        public int qtde { get; set; }
    }
}
