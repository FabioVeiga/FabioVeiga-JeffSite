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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataAniversario { get; set; }
        
        [Display(Name = "Data Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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