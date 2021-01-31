using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace JeffSite.Models.Livro
{
    public class WhereToBuy
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Por favor, inserir {0}!")]
        [Display(Name = "Onde comprar")]
        public string Name { get; set; }
        
        [Display(Name = "Endereço da rede social")]
        public string Url { get; set; }

        [Display (Name= "Escolha um icone")]
        public string IconFA { get; set; }
        public Livro livro { get; set; }
        
    }
}
