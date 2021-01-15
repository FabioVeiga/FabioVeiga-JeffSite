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

    }

}
