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
using JeffSite.Models.Loja;
using JeffSite.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JeffSite.Controllers
{
    
    [Route("api/livro")]
    public class LojaControllerApi: Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LivroService _livroService;
        private readonly LojaService _lojaService;
        private readonly MallingService _mallingService;

        public LojaControllerApi(SocialMidiaService socialMidia, LivroService livroService, LojaService lojaService, MallingService mallingService)
        {
            _socialMidia = socialMidia;
            _livroService = livroService;
            _lojaService = lojaService;
            _mallingService = mallingService;
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

        [Route("add-pedido")]
        [HttpPost]
        public IActionResult AddPedido([FromBody]Pedido pedido){
            pedido.Status = Status.Aguardando_Link_De_Pagamento;
            _lojaService.AddPedido(pedido);

            //add email malling
            var mail = new Malling();
            mail.Email = pedido.Email;
            mail.Nome = pedido.Nome;

            //Add malling
            if(!_mallingService.CheckMail(mail)){
                _mallingService.AddMalling(mail);
            }

            return Ok();
        }

    }
}
