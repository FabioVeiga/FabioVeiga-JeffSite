using System;
using System.ComponentModel.DataAnnotations;

namespace JeffSite_WF_472.Models
{
    public class Malling
    {
        [Key]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(100)]
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