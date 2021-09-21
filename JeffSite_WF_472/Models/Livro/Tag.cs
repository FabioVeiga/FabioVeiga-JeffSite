using System.ComponentModel.DataAnnotations;

namespace JeffSite_WF_472.Models.Livro
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        
        [Required (ErrorMessage = "Por favor, inserir {0}!")]
        [Display(Name = "Tag para pesquisa")]
        public string Name { get; set; }
        public int IdLivro { get; set; }

        public Tag(){}

        public Tag(int id, string name){
            Id = id;
            Name = name;
        }
        
    }
}