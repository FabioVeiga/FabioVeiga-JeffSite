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

    }

}
