using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JeffSite_WF_472.Models.Loja.Api
{
    public class ItemWhereToBuy
    {
        public int LivroId { get; set; }
        public string Name { get; set; }
        public string IconFa { get; set; }
        public string UrlEndereco { get; set; }
    }
}