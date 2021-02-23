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

        private readonly MallingService _mallingService;

        public MallingController(MallingService mallingService)
        {
            _mallingService = mallingService;
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
        
    }
}