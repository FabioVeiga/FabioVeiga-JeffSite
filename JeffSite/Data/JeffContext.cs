using System;
using JeffSite.Models;
using JeffSite.Models.Livro;
using Microsoft.EntityFrameworkCore;

namespace JeffSite.Data
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

    }
}
