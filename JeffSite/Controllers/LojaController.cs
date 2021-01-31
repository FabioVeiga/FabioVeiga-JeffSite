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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Livro livro, IFormFile Img){
            int proxId = _livroService.FindNextIdLivro();
            livro.ImgName = string.Concat(proxId,"_",Img.FileName);
            string path = Path.Combine("wwwroot","img","Livro",livro.ImgName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                Img.CopyTo(stream); 
            }
            var item = _livroService.Create(livro, proxId);
            return RedirectToAction("CreateWhereToBuy", item);
        }

        [HttpGet]
        public IActionResult Delete(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Deletar";
            var livro = _livroService.FindById(id);
            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Livro livro){
            string path = Path.Combine("wwwroot","img","Livro",livro.ImgName);
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            file.Delete();
            _livroService.Delete(livro);
            return RedirectToAction("ListLivros");
        }

        [HttpGet]
        public IActionResult CreateWhereToBuy(Livro livro){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Adicionar URL de compra";
            var links = _livroService.FindAllWhereToBuyById(livro.Id);
            return View(links);
        }

        [Route("AddWhereToBuy")]
        [HttpGet]
        public IActionResult CreateWhereToBuy(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Adicionar URL de compra";
            var livro = _livroService.FindById(id);
            ViewBag.Links = _livroService.FindAllWhereToBuyById(livro.Id);
            return View(livro);
        }
    }
}
