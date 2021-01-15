using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models.Livro
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        
        [Required (ErrorMessage = "Por favor, inserir {0}!")]
        [Display(Name = "Tag para pesquisa")]
        public string Name { get; set; }
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();

        public Tag(){}

        public Tag(int id, string name){
            Id = id;
            Name = name;
        }
        
    }
}