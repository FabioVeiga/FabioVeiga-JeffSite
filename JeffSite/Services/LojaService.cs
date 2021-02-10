using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Data;


namespace JeffSite.Services
{
    public class LojaService
    {
        private readonly JeffContext _context;

        public LojaService(JeffContext context)
        {
            _context = context;
        }
    }

}
