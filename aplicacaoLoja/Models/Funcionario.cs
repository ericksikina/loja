using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace aplicacaoLoja.Models
{
    [Table("Funcionarios")]
    public class Funcionario
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido")]
        [StringLength(40)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(18)]
        [Display(Name = "Telefone")]
        public string telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(14)]
        [Display(Name = "Cpf")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Salário")]
        public double salario { get; set; }
    }
}
