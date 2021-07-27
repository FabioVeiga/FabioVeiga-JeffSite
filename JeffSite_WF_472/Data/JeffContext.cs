using JeffSite_WF_472.Models;
using JeffSite_WF_472.Models.Livro;
using JeffSite_WF_472.Models.Loja;
using Microsoft.EntityFrameworkCore;

namespace JeffSite_WF_472.Data
{
    public class JeffContext : DbContext
    {
        public JeffContext(DbContextOptions<JeffContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<SocialMidia> SocialMidia { get; set; }
        public DbSet<Configuracao> Configuracao { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<Leitor> Leitors { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<WhereToBuy> WhereToBuys { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Malling> Mallings { get; set; }

    }
}
