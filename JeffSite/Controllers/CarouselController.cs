using System.Globalization;
using System.Xml.Schema;

using JeffSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JeffSite.Services;
using System.Threading.Tasks;
using System.IO;
using System;

namespace JeffSite.Controllers
{
    public class CarouselController : Controller
    {
        private readonly CarouselService _carouselService;

        public CarouselController(CarouselService carouselService){
            _carouselService = carouselService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Lista de Carrousel";
            var lista = _carouselService.FindAll();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
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
        public IActionResult Create(Carousel carousel, IFormFile img){
            if(img != null){
                carousel.Image = img.FileName;
            }
            if(ModelState.IsValid){
                var path = $@"{carousel.PathImage}/{carousel.Image}";
                using (var stream = new FileStream(path , FileMode.Create))
                {
                    img.CopyTo(stream); 
                }
                if(carousel.ExpirationDate == null){
                    carousel.ExpirationDate = new DateTime(1900,01,01);
                }
                _carouselService.Create(carousel);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Create));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Excluir";
            var item = _carouselService.FindById(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Carousel carousel){
            var item = _carouselService.FindById(carousel.Id);
            var pathimg = $@"{item.PathImage}{item.Image}";
            System.IO.FileInfo file = new System.IO.FileInfo(pathimg);
            try{
                file.Delete();
                _carouselService.Delete(item);
            }catch(System.IO.IOException e){
                throw new System.Exception(e.Message);
            }

            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Editar";
            var item = _carouselService.FindById(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Carousel carousel, IFormFile img){
            var pathimg = $@"{carousel.PathImage}{carousel.Image}";
            System.IO.FileInfo file = new System.IO.FileInfo(pathimg);
            if(img != null){
                try{
                    var pathimgNew = $@"{carousel.PathImage}{img.FileName}";
                    file.Delete();
                    using (var stream = new FileStream(pathimgNew , FileMode.Create))
                    {
                        img.CopyTo(stream); 
                    }
                    carousel.Image = img.FileName;
                }catch(System.IO.IOException e){
                    throw new System.Exception(e.Message);
                }
            }
            if(carousel.ExpirationDate == null){
                carousel.ExpirationDate = new DateTime(1900,01,01);
            }
            _carouselService.Edit(carousel);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
