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
    
    [Route("api/livro")]
    public class LojaControllerApi: Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LivroService _livroService;
        public LojaControllerApi(SocialMidiaService socialMidia, LivroService livroService)
        {
            _socialMidia = socialMidia;
            _livroService = livroService;
        }


        [Route("addwheretobuy")]
        [HttpPost]
        public IActionResult AddWhereToBuy(WhereToBuy item){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            if(item == null){
                return BadRequest();
            }
            _livroService.AddWhereToBuy(item);
            return Ok(item);
        }

    }
}
