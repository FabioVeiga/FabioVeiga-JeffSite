using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Data;
using JeffSite.Models;

namespace JeffSite.Services
{
    public class CarouselService
    {
        private readonly JeffContext _context;

        public CarouselService(JeffContext context)
        {
            _context = context;
        }

        public List<Carousel> FindAll(){
            return _context.Carousels.ToList();
        }

        public Carousel FindById(int id){
            return  _context.Carousels.FirstOrDefault(x => x.Id == id);
        }

        public void Create(Carousel carousel){
            _context.Add(carousel);
            _context.SaveChanges();
        }

        public void Delete(Carousel carousel){
            _context.Remove(carousel);
            _context.SaveChanges();
        }

        public void Edit(Carousel carousel){
            _context.Update(carousel);
            _context.SaveChanges();
        }

    }

}
