using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace aplicacaoLoja.Models
{
    [Table("Fornecedores")]
    public class Fornecedor
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(40)]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(17)]
        [Display(Name = "Telefone")]
        public string telefone { get; set; }

        [Display(Name = "Endereco")]
        [StringLength(80)]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string endereco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(20)]
        [Display(Name = "Cnpj")]
        public string cnpj { get; set; }
    }
}
