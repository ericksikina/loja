using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace aplicacaoLoja.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(60)]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string nome { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(15)]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string telefone { get; set; }

        [Display(Name = "CPF")]
        [StringLength(14)]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string cpf { get; set; }
    }
}
