using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
        [Display(Name = "Nome da imagem")]
        public string ImgName { get; set; }

        [Display(Name = "Livro esgotado?")]
        public bool Esgotado { get; set; }
        
        

        public virtual ICollection<WhereToBuy> WhereToBuys { get; set; }
        public Livro()
        {
            WhereToBuys = new HashSet<WhereToBuy>();
        }
        
    }
    
}