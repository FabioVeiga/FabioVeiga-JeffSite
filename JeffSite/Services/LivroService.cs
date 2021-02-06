using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Data;
using JeffSite.Models.Livro;

namespace JeffSite.Services
{
    public class LivroService
    {
        private readonly JeffContext _context;

        public LivroService(JeffContext context)
        {
            _context = context;
        }

        public List<Livro> FindAll(){
            return _context.Livros.ToList();
        }

        public int FindNextIdLivro(){
            return _context.Livros.Any() ? _context.Livros.Max(x => x.Id) + 1 : 1;
        }

        public Livro Create(Livro livro, int id){
            _context.Livros.Add(livro);
            _context.SaveChanges();
            return _context.Livros.FirstOrDefault(x => x.Id == id);
        }

        public Livro FindById(int id){
            return _context.Livros.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Livro livro){
            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }

        public void AddWhereToBuy(WhereToBuy item){
            _context.WhereToBuys.Add(item);
            _context.SaveChanges();
        }

        public List<WhereToBuy> FindAllWhereToBuyById(int idLivro){
            return _context.WhereToBuys.Where(x => x.Livro.Id == idLivro).ToList();
        }

        public WhereToBuy FindLastWhereToBuy(int idLivro){
            return _context.WhereToBuys.OrderByDescending(x => x.Id).Where(x => x.Livro.Id == idLivro).FirstOrDefault();
        }

    }

}
