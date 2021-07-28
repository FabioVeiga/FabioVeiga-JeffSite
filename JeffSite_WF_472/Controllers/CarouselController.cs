using JeffSite_WF_472.Models;
using JeffSite_WF_472.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Web.Mvc;


namespace JeffSite_WF_472.Controllers
{
    public class CarouselController : Controller
    {
        private readonly CarouselService _carouselService;
        private readonly LeitorService _leitorService;

        public CarouselController(CarouselService carouselService, LeitorService leitorService){
            _carouselService = carouselService;
            _leitorService = leitorService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var userLogged = Session["userLogged"].ToString();
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Lista de Carrousel";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var lista = _carouselService.FindAll();
            return View(lista);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var userLogged = Session["userLogged"].ToString();
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Novo";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carousel carousel, IFormFile img){
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
        public ActionResult Delete(int id)
        {
            var userLogged = Session["userLogged"].ToString();
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Excluir";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _carouselService.FindById(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Carousel carousel){
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
        public ActionResult Edit(int id)
        {
            var userLogged = Session["userLogged"].ToString();
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Editar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _carouselService.FindById(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Carousel carousel, IFormFile img){
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
