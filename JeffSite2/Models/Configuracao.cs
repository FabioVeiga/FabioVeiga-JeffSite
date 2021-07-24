using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models
{
    public class Configuracao
    {
        [Key]
        public int Cod { get; set; }
        
        [Required (ErrorMessage = "Por favor, inserir {0}!")]
        [Display(Name = "E-mail para contato")]
        [EmailAddress (ErrorMessage = "Necessário email válido!")]
        public string ContactEmail { get; set; }
        [Display(Name = "Imagem do perfil")]
        public string ImgProfile { get; set; }

        [Display(Name = "Foto do logotipo")]
        public string ImgLogo { get; set; }

        [Display(Name = "Endereco da loja do Mercado Livre")]
        public string UrlMercadoLivre { get; set; }
        
        [Display(Name = "Endereco do Site")]
        public string UrlSite { get; set; }

        [Display(Name = "Nome do Site")]
        public string NomeSite { get; set; }

        public Configuracao()
        {
        }

        public Configuracao(string contactEmail, string imgProfile, string imgLogo, string urlMercadoLivre, string urlSite, string nomeSite)
        {
            ContactEmail = contactEmail;
            ImgProfile = imgLogo;
            ImgLogo = imgLogo;
            UrlMercadoLivre = urlMercadoLivre;
            UrlSite = urlSite;
            NomeSite = nomeSite;
        }
        
    }
}
