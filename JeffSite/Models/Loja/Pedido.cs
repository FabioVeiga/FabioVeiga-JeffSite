using System;
using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models.Loja{
    public class Pedido{
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string NomeDedicado { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string LinkRastreio { get; set; }
        public string LinkPagamento { get; set; }
        public Status Status { get; set; }

        public Pedido(){}
        
    }
}