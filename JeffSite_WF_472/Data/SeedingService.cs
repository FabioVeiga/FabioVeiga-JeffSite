using System.Linq;
using JeffSite_WF_472.Models;

namespace JeffSite_WF_472.Data
{
    public class SeedingService
    {
        private readonly JeffContext _context;

        public SeedingService(JeffContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.User.Any())
            {
                return;
            }

            User user1 = new User {UserName = "Admin", Pass = Utils.Util.GerarHashMd5("123")};
            User user2 = new User {UserName = "JeffAdmin", Pass = Utils.Util.GerarHashMd5("Admin0123")};
            _context.User.AddRange(user1, user2);

            if (_context.Configuracao.Any())
            {
                return;
            }

            Configuracao c1 = new Configuracao {Cod = 1, ImgLogo = "imgLogo.png", ImgProfile = "imgProfile.png", ContactEmail = "test@test.com"};
            _context.Configuracao.Add(c1);

            _context.SaveChanges();

            if (_context.Emails.Any())
            {
                return;
            }

            Email e1 = new Email {
                Id = 1, 
                Servidor = "in-v3.mailjet.com", 
                Porta = 587,
                UsarCredencialPadrao = false,
                ContaEmail = "82e27f3e2c23efde495f3d945230903e",
                Senha = "0291dad12068416508fa09631f8c7e2e",
                HabilitaSSL = true
                };
            _context.Add(e1);
            _context.SaveChanges();

        }
    }
}
