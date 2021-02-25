using System.Security.Cryptography.X509Certificates;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JeffSite.Controllers
{
    public class MallingController : Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly MallingService _mallingService;

        public MallingController(MallingService mallingService, SocialMidiaService socialMidia)
        {
            _mallingService = mallingService;
            _socialMidia = socialMidia;
        }

        [HttpGet]
        public IActionResult Index(){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            var itens = _mallingService.FillAllMalling();
            ViewData["Title"] = "Lista de email";
            return View(itens);
        }

        [HttpGet]
        public IActionResult EnviarEmailMailling(){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Enviar email mailling";
            return View();
        }

        [HttpGet]
        [Route("remover-email/{email}")]
        public IActionResult RemoverEmail(string email){
            ViewBag.Redes = _socialMidia.FindAll();
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("remover-email")]
        public IActionResult RemoverEmail(bool opcao, string email){
            ViewBag.Redes = _socialMidia.FindAll();
            
            if(opcao){
                _mallingService.DeleteEmail(email);
                ViewBag.Message = "Email removido!";
            }else{
                ViewBag.Message = "Email mantido!";
            }

            return RedirectToAction("RemoverEmail",email);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EnviarEmailMalling(string titulo, string html){
            if(string.IsNullOrEmpty(titulo) && string.IsNullOrEmpty(html)){
                ViewBag.Obrigatorio = "Campo obrigatorio!";
                return View("EnviarEmailMailling", titulo);
            }

            return View("EnviarEmailMailling");
        }

        
        
    }
}