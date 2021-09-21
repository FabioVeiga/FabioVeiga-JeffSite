using System.ComponentModel.DataAnnotations;

namespace JeffSite_WF_472.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Por favor, inserir seu {0}!")]
        [MaxLength(100)]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Por favor, inserir sua {0}!")]
        [MinLength(6, ErrorMessage = "{0} requer no minimo {1} caracteres!")]
        [Display(Name = "Senha")]
        public string Pass { get; set; }

        public User()
        {
        }

        public User(string user, string pass)
        {
            UserName = user;
            Pass = pass;
        }
    }
}