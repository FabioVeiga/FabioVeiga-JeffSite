using System;
using System.Reflection.PortableExecutable;
namespace JeffSite.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Servidor { get; set; }
        public int Porta { get; set; }
        public string ContaEmail { get; set; }
        public string Senha { get; set; }
        public bool HabilitaSSL { get; set; }
        
    }
}