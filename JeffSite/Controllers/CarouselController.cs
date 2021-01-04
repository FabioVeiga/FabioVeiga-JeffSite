using System.Globalization;
using System.Xml.Schema;

using JeffSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JeffSite.Services;
using System.Threading.Tasks;
using System.IO;

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
                _carouselService.Create(carousel);
                return View(nameof(Index));
            }
            return View(nameof(Index));
        }
    }
}
