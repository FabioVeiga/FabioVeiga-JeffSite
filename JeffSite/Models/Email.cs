using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
namespace JeffSite.Models
{
    public class Email
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        public string Servidor { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        public int Porta { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        public string ContaEmail { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        public string Senha { get; set; }
        public bool HabilitaSSL { get; set; }
        
    }
}