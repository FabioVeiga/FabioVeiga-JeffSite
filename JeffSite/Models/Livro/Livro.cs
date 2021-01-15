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
        public string ImgName { get; set; }

        public int IdWhereToBuy { get; set; }
        public WhereToBuy WhereToBuy { get; set; }
        
        public int IdTag{ get; set; }
        public Tag Tag { get; set; }

        public Livro(){}

        public Livro(int id, string title, string description, string imgName){
            Id = id;
            Title = title;
            Description = description;
            ImgName = imgName;
        }
        
    }
    
}