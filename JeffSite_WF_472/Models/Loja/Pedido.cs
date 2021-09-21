using System.ComponentModel.DataAnnotations;

namespace JeffSite_WF_472.Models.Loja{
    public class Pedido{
        [Display(Name = "Cod Pedido")]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Nome do dedicado")]
        public string NomeDedicado { get; set; }
        public string CEP { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }

        [Display(Name = "Link/Codigo do Rastreio")]
        public string LinkRastreio { get; set; }

        [Display(Name = "Link de Pagamento")]
        [Url(ErrorMessage = "Inserir um endereço valido!")]
        public string LinkPagamento { get; set; }
        public Status Status { get; set; }
        public int LivroId { get; set; }

        public Pedido(){}
        
    }
}