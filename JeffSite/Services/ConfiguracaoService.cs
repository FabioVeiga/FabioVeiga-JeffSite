using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Data;
using JeffSite.Models;

namespace JeffSite.Services
{
    public class ConfiguracaoService
    {
        private readonly JeffContext _context;

        public ConfiguracaoService(JeffContext context)
        {
            _context = context;
        }

        public Configuracao Find(){
            return _context.Configuracao.FirstOrDefault();
        }

        public void Edit(Configuracao configuracao){
            _context.Configuracao.Update(configuracao);
            _context.SaveChanges();
        }

        public string FindAdminEmail(){
            return _context.Configuracao.FirstOrDefault().ContactEmail;
        }

    }

}
