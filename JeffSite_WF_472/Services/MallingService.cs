using JeffSite_WF_472.Data;
using JeffSite_WF_472.Models;
using System.Collections.Generic;
using System.Linq;

namespace JeffSite_WF_472.Services
{
    public class MallingService
    {
        private readonly JeffContext _context;

         public MallingService(JeffContext context)
        {
            _context = context;
        }

        public void AddMalling(Malling mail){
            _context.Add(mail);
            _context.SaveChanges();
        }

        public bool CheckMail(Malling mail){
            var teste = _context.Mallings.FirstOrDefault(x => x.Email == mail.Email);
            if(teste == null){
                return false;
            }else{
                return true;
            }
        }

        public List<Malling> FillAllMalling(int limit){
            return _context.Mallings
            .Take(limit)
            .ToList();
        }

        public List<Malling> FillAllMallingWithFilters(int limit, string filtro){
            switch (filtro)
            {
                case "Aniversario":
                    return _context.Mallings
                    .Where(x => x.DataAniversario != null)
                    .Take(limit)
                    .ToList();
                default:
                    return _context.Mallings
                    .Where(x => x.Onde == filtro)
                    .Take(limit)
                    .ToList();
            }
            
        }

        public List<string> FillAllMallingJusEmail(){
            return _context.Mallings.Select(x => x.Email).ToList();
        }

        public void DeleteEmail(string email){
            var e = _context.Mallings.FirstOrDefault(x => x.Email == email);
            _context.Mallings.Remove(e);
            _context.SaveChanges();
        }
    }
}