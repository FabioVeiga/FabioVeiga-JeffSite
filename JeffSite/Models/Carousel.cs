using System;
using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models
{
    public class Carousel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime ExpirationDate { get; set; }
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
