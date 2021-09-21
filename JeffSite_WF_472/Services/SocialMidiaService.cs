using JeffSite_WF_472.Data;
using JeffSite_WF_472.Models;
using System.Collections.Generic;
using System.Linq;

namespace JeffSite_WF_472.Services
{
    public class SocialMidiaService
    {
        private readonly JeffContext _context;

        public SocialMidiaService(JeffContext context)
        {
            _context = context;
        }
        public List<SocialMidia> FindAll(){
            return _context.SocialMidia.ToList();
        }
        public void Create(SocialMidia socialMidia){
            _context.SocialMidia.Add(socialMidia);
            _context.SaveChanges();
        }
        public SocialMidia FindById(int id){
            return _context.SocialMidia.FirstOrDefault(s => s.Id == id);
        }
        public void Delete(SocialMidia socialMidia){
            _context.SocialMidia.Remove(socialMidia);
            _context.SaveChanges();
        }

        public void Edit(SocialMidia socialMidia){
            _context.SocialMidia.Update(socialMidia);
            _context.SaveChanges();
        }

    }

}
