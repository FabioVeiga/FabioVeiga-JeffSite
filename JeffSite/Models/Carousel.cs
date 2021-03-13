using System;
using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models
{
    public class Carousel
    {
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Titulo obrigatoria!")]
        [MinLength(3,ErrorMessage = "Por favor, inserir mais de {1} caracteres!")]
        public string Title { get; set; }

        [Display(Name = "Breve Descrição")]
        public string Description { get; set; }
        public string Link { get; set; }

        [Display(Name = "Data de Expiração")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Imagem")]
        [Required(ErrorMessage = "Imagem obrigatoria!")]
        public string Image { get; set; }
        public string PathImage { get; set; }
        
        public Carousel(){}

        public Carousel(int id, string title, string description, string link, DateTime expirationDate, string image, string pathImg){
            Id = id;
            Title = title;
            Description = description;
            Link = link;
            ExpirationDate = expirationDate;
            Image = image;
            PathImage = pathImg;
        }
        
    }
}
