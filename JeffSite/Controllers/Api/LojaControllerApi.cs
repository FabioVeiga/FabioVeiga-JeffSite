using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Models.Livro;
using JeffSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JeffSite.Controllers
{
    
    
    public class LojaControllerApi: Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LivroService _livroService;
        public LojaControllerApi(SocialMidiaService socialMidia, LivroService livroService)
        {
            _socialMidia = socialMidia;
            _livroService = livroService;
        }


        [Route("api/AddWhereToBuy/iconFa/name/url/idLivro")]
        public IActionResult AddWhereToBuy(string iconFa, string name, string url, int idLivro){
            WhereToBuy item = new WhereToBuy{
                IconFA = iconFa,
                Name = name,
                Url = url,
                IdLivro = idLivro
            };
            _livroService.AddWhereToBuy(item);
            var links = _livroService.FindAllWhereToBuyById(idLivro);
            return Ok(links);
        }

    }
}
