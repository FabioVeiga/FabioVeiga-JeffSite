using System.IO;
using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models
{
    public class Leitor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor, informe um {0}")]
        [MinLength(3,ErrorMessage = "Deve conter no minimo {1}")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Por favor, informe um {0}")]
        [EmailAddress(ErrorMessage = "O {0} deve ser valido")]
        public string Email { get; set; }
        
        [Display(Name = "Apelido")]
        public string Nickname { get; set; }

        [Display(Name = "Imagem")]
        [Required(ErrorMessage = "Por favor, selecione uma {0}")]
        public FileInfo Img { get; set; }
        
        [Display(Name = "Esta aprovado")]
        public bool IsApproved { get; set; }
        
    }
}
