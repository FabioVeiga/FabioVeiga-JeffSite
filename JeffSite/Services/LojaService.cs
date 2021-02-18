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

        public void AddPedido(Pedido pedido){
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }
    }

}
