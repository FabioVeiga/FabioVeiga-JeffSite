using System.ComponentModel.DataAnnotations;

namespace JeffSite_WF_472.Models.Livro
{
    public class WhereToBuy
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Por favor, inserir {0}!")]
        [Display(Name = "Onde comprar")]
        public string Name { get; set; }
        
        [Display(Name = "Endereço da rede social")]
        public string UrlEndereco { get; set; }

        [Display (Name= "Escolha um icone")]
        public string IconFA { get; set; }
        public virtual Livro Livro { get; set; }
        public int LivroId { get; set; }

    }
}
