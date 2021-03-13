using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Data;
using JeffSite.Models.Loja;

namespace JeffSite.Services
{
    public class LojaService
    {
        private readonly JeffContext _context;

        public LojaService(JeffContext context)
        {
            _context = context;
        }

        public int FindNextIdPedido(){
            return _context.Pedidos.Any() ? _context.Pedidos.Max(x => x.Id) + 1 : 1020;
        }

        public void AddPedido(Pedido pedido){
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        public int HowManyLivros(){
            return _context.Livros.ToList().Count();
        }

        public int HowManyPedidos(){
            return _context.Pedidos.ToList().Count();
        }
    }

}
