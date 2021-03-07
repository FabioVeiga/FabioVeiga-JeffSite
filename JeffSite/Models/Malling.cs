using System;
using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models
{
    public class Malling
    {
        [Key]
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Onde { get; set; }

        [Display(Name = "Aniversário")]
        public DateTime? DataAniversario { get; set; }
        
        [Display(Name = "Data Cadastro")]
        public DateTime? DataCadastro { get; set; }

        public Malling(){
            
        }

        public Malling(string email, string nome)
        {
            Email = email;
            Nome = nome;
        }
        
    }
}