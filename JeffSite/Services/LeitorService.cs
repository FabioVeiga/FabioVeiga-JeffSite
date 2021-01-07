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
        

    }

}
