using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models.Livro
{
    public class Livro{
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Por favor, inserir {0}!")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Required (ErrorMessage = "Por favor, inserir {0}!")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        public string ImgName { get; set; }
        
        public Tag Tags { get; set; }

        public WhereToBuy WhereToBuy { get; set; }
        
        
    }
    
}