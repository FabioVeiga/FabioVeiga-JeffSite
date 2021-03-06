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
            .OrderByDescending(x => x.Id)
            .ToList();
        }
        public List<Leitor> FindAllApproved(bool flag){
            return _context.Leitors
            .Where(x => x.IsApproved == flag)
            .OrderByDescending(x => x.Id)
            .ToList();
        }

        public List<Leitor> FindAllApproved(int limit){
            return _context.Leitors
            .Where(x => x.IsApproved == true)
            .OrderByDescending(x => x.Id)
            .Take(limit)
            .ToList();
        }

        public async Task CreateAsync(Leitor leitor){
            leitor.IsApproved = false;
            leitor.CreateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            await _context.Leitors.AddAsync(leitor);
            await _context.SaveChangesAsync();
        }

        public Leitor FindById(int id){
            return  _context.Leitors.FirstOrDefault(x => x.Id == id);
        }

        public async Task ApprovePostAsync(int id){
            Leitor leitor = _context.Leitors.FirstOrDefault(x => x.Id == id);
            leitor.IsApproved = true;
            _context.Leitors.Update(leitor);
            await _context.SaveChangesAsync();
        }

        public void DisapprovePostAsync(Leitor leitor){
            _context.Leitors.Remove(leitor);
            _context.SaveChangesAsync();
        }

        public int HowManyPostsApproved(){
            return _context.Leitors.Where(x => x.IsApproved == true).ToList().Count;
        }

        public int HowManyPostsAreNotApproved(){
            return _context.Leitors.Where(x => x.IsApproved == false).ToList().Count;
        }

    }

}
