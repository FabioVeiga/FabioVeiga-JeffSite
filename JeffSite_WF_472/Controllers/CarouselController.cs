using JeffSite_WF_472.Models;
using JeffSite_WF_472.Services;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;


namespace JeffSite_WF_472.Controllers
{
    public class CarouselController : Controller
    {
        private readonly CarouselService _carouselService;
        private readonly LeitorService _leitorService;
        private UserLogged _userLogged;
        private string pathRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
        private string pathCarousel = Path.Combine("Content","img", "Carousel");

        public CarouselController(CarouselService carouselService, LeitorService leitorService, UserLogged userLogged)
        {
            _carouselService = carouselService;
            _leitorService = leitorService;
            _userLogged = userLogged;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Lista de Carrousel";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var lista = _carouselService.FindAll();
            return View(lista);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Novo";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Carousel carousel, HttpPostedFileBase Image){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            if(ModelState.IsValid){

                pathRoot += Path.Combine(pathCarousel, Image.FileName);
                Image.SaveAs(pathRoot);

                if (carousel.ExpirationDate == null){
                    carousel.ExpirationDate = new DateTime(1900, 01, 01);
                }
                
                _carouselService.Create(carousel);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Create));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Excluir";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _carouselService.FindById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(Carousel carousel){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");
            
            try{
                var item = _carouselService.FindById(carousel.Id);
                pathRoot += Path.Combine(pathCarousel, item.Image);
                FileInfo file = new FileInfo(pathRoot);
                file.Delete();
                _carouselService.Delete(item);
            }catch(IOException e){
                throw new Exception(e.Message);
            }

            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Editar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _carouselService.FindById(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Carousel carousel, HttpPostedFileBase Image){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");
            
            if (Image != null){
                try{
                    pathRoot += Path.Combine(pathCarousel, Image.FileName);
                    FileInfo file = new FileInfo(pathRoot);
                    file.Delete();
                    Image.SaveAs(pathRoot);
                    carousel.Image = Image.FileName;
                }catch(IOException e){
                    throw new Exception(e.Message);
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
