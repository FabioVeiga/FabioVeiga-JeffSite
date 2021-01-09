using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Data;
using JeffSite.Models;

namespace JeffSite.Services
{
    public class LeitorService
    {
        private readonly JeffContext _context;

        public LeitorService(JeffContext context)
        {
            _context = context;
        }
        
        public List<Leitor> FindAll(){
            return _context.Leitors
            .OrderByDescending(x => x.CreateDate)
            .ToList();
        }

        public async Task CreateAsync(Leitor leitor){
            leitor.IsApproved = false;
            leitor.CreateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            await _context.Leitors.AddAsync(leitor);
            await _context.SaveChangesAsync();
        }

        public async Task Approved(int id){
            Leitor leitor = _context.Leitors.FirstOrDefault(x => x.Id == id);
            leitor.IsApproved = true;
            _context.Leitors.Update(leitor);
            await _context.SaveChangesAsync();
        }

    }

}
