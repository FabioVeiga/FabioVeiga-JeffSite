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
            livro.WhereToBuys = _livroService.FindAllWhereToBuyById(livro.Id);
            return View(livro);
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
            livro.WhereToBuys = _livroService.FindAllWhereToBuyById(livro.Id);
            return View(livro);
        }


        [HttpGet]
        public IActionResult EditWhereToBuy(int id, int idLivro){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Editar URL de compra";
            var item = _livroService.FindWhereToBuyById(id);
            item.Livro = _livroService.FindById(idLivro);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWhereToBuy(WhereToBuy item){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            item.Livro = _livroService.FindById(item.Livro.Id);
            await _livroService.UpdateWhereToBuyAsync(item);

            return RedirectToAction("CreateWhereToBuy", item.Livro);
        }


        [HttpGet]
        public IActionResult DeleteWhereToBuy(int id, int idLivro){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Deletar URL de compra";
            var item = _livroService.FindWhereToBuyById(id);
            item.Livro = _livroService.FindById(idLivro);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWhereToBuy(WhereToBuy item){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            item.Livro = _livroService.FindById(item.Livro.Id);
            await _livroService.DeleteWhereToBuyAsync(item);

            return RedirectToAction("CreateWhereToBuy", item.Livro);
        }

    }
}
