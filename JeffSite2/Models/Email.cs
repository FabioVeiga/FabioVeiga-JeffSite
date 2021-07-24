using System.ComponentModel.DataAnnotations;
namespace JeffSite.Models
{
    public class Email
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        [Display (Name = "URL do servidor")]
        public string Servidor { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        public int Porta { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        [Display (Name = "Email Crendencial")]
        public string ContaEmail { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        [Display (Name = "Senha Crendencial")]
        public string Senha { get; set; }

        [Display (Name = "Habilitar SSL?")]
        public bool HabilitaSSL { get; set; }

        [Display (Name = "Usar Credencial Padrao")]
        public bool UsarCredencialPadrao { get; set; }
        
        
        
    }
}