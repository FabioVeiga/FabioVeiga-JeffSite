using System;
using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models
{
    public class Carousel
    {
        public int Id { get; set; }
        [Display(Name = "Titulo")]
        [MinLength(3,ErrorMessage = "Por favor, inserir mais de {1} caracteres!")]
        public string Title { get; set; }
        [Display(Name = "Breve Descrição")]
        public string Description { get; set; }
        public string Link { get; set; }
        [Display(Name = "Data de Expiração")]
        public DateTime ExpirationDate { get; set; }
        [Required(ErrorMessage = "Imagem obrigatoria!")]
        [Display(Name = "Imagem")]
        public string Image { get; set; }
        
        public Carousel(){}

        public Carousel(int id, string title, string description, string link, DateTime expirationDate, string image){
            Id = id;
            Title = title;
            Description = description;
            Link = link;
            ExpirationDate = expirationDate;
            Image = image;
        }
        
    }
}
