using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Data;
using JeffSite.Models.Livro;
using JeffSite.Models.Loja;

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

        public void Edit(Livro livro){
            _context.Livros.Update(livro);
            _context.SaveChanges();
        }

        public async Task AddWhereToBuyAsync(WhereToBuy item){
            await _context.WhereToBuys.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public List<WhereToBuy> FindAllWhereToBuyByIdLivro(int idLivro){
            return _context.WhereToBuys.Where(x => x.Livro.Id == idLivro).ToList();
        }

        public WhereToBuy FindWhereToBuyById(int id){
            return _context.WhereToBuys.Where(x => x.Id == id).FirstOrDefault();
        }

        public WhereToBuy FindLastWhereToBuy(int idLivro){
            return _context.WhereToBuys.Where(x => x.Livro.Id == idLivro).OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public async Task DeleteWhereToBuyAsync(WhereToBuy item){
            _context.WhereToBuys.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWhereToBuyAsync(WhereToBuy item){
            _context.WhereToBuys.Update(item);
            await _context.SaveChangesAsync();
        }

        public List<Pedido> FindAllPedidosByIdLivro(int idLivro){
            return _context.Pedidos.Where(x => x.LivroId == idLivro).ToList();
        }

    }

}
