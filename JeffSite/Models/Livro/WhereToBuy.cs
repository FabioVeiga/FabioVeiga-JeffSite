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
        public string ListWhereToBuy { get; set; }
        public ICollection<Livro> Livros { get; set; }
        
        
    }
}
