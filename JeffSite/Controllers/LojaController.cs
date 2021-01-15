using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JeffSite.Controllers
{
    
    
    public class LojaController : Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LivroService _livroService;
        private const string titlePage = "Loja";
        public LojaController(SocialMidiaService socialMidia, LivroService livroService)
        {
            _socialMidia = socialMidia;
            _livroService = livroService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Redes = _socialMidia.FindAll();
            ViewBag.Title = titlePage;
            return View();
        }

        public IActionResult ListLivros(){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Livro";
            var livros = _livroService.FindAll();
            return View(livros);
        }
        
        [HttpGet]
        public IActionResult Create(){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Novo";
            return View();
        }
    }
}
