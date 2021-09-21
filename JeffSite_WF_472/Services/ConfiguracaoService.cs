using JeffSite_WF_472.Data;
using JeffSite_WF_472.Models;
using System.Linq;

namespace JeffSite_WF_472.Services
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

        public Email FindEmail(){
            return _context.Emails.FirstOrDefault();
        }

        public void EditEmail(Email email){
            _context.Emails.Update(email);
            _context.SaveChanges();
        }

    }

}
