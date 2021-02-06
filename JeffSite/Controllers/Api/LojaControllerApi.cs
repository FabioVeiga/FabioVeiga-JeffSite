using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Models.Livro;
using JeffSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<IActionResult> CreateWhereToBuy(WhereToBuy item){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            
            item.Livro = _livroService.FindById(item.Livro.Id);
            await _livroService.AddWhereToBuyAsync(item);

            return Ok();
        }

        [Route("findlastwheretobuy/{idlivro}")]
        [HttpGet]
        public IActionResult FindLastWhereToBuy(int idLivro){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            
            var item = _livroService.FindLastWhereToBuy(idLivro);

            return Ok(item);
        }

        [Route("removewheretobuy/{id}/{idLivro}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteWhereToBuy(int id, int idLivro){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            var item = _livroService.FindWhereToBuyById(id);
            item.Livro = _livroService.FindById(idLivro);
            await _livroService.DeleteWhereToBuyAsync(item);

            return Ok();
        }

    }
}
