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
        
        public int IdLivro { get; set; }
        public ICollection<Livro> Livros { get; set; }
        
        public WhereToBuy(){}
        public WhereToBuy(int id, string name, string url, string iconFA){
            Id = id;
            Name = name;
            Url = url;
            IconFA = iconFA;
        }
    }
}
