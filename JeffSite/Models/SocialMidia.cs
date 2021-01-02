using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models
{
    public class SocialMidia
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Por favor, inserir {0}!")]
        [Display(Name = "Nome rede social")]
        public string Name { get; set; }

        [Display(Name = "Endereço da rede social")]
        public string Url { get; set; }

        [Display (Name= "Escolha um icone")]
        public string IconFA { get; set; }

        public SocialMidia()
        {
        }

        public SocialMidia(string name, string url, string iconFA)
        {
            Name = name;
            Url = url;
            IconFA = iconFA;
        }
        
    }
}
