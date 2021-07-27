using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite_WF_472.Data;
using JeffSite_WF_472.Models;

namespace JeffSite_WF_472.Services
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

        public List<Carousel> FindAllActive(){
            string now = DateTime.Now.ToString("yyyy-MM-dd");
            return _context.Carousels
            .Where(x => (x.ExpirationDate == new DateTime(1900,01,01)) || x.ExpirationDate >= DateTime.Parse(now))
            .ToList();
        }

        public int Quantity(){
            string now = DateTime.Now.ToString("yyyy-MM-dd");
            return _context.Carousels
            .Where(x => (x.ExpirationDate == new DateTime(1900,01,01)) || x.ExpirationDate >= DateTime.Parse(now))
            .ToList()
            .Count();
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
