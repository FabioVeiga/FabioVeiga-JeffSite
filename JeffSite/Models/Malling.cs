using System.ComponentModel.DataAnnotations;

namespace JeffSite.Models
{
    public class Malling
    {
        [Key]
        public string Email { get; set; }
        public string Nome { get; set; }
        
    }
}