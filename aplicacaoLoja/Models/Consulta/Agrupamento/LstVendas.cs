namespace aplicacaoLoja.Models.Consulta.Agrupamento
{
    public class LstVendas
    {
        public int id { get; set; }
        public DateTime data { get; set; }
        public int clienteId { get; set; }
        public String cliente { get; set; }
        public String funcionario { get; set; }
        public String produto { get; set; }
        public int quantidade { get; set; }
        public decimal total { get; set; }
    }
}
