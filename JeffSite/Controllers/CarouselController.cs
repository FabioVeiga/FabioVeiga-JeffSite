
using JeffSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JeffSite.Services;
using System.Threading.Tasks;

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
                return View("AdminHome");
            }
            ViewData["Title"] = "Lista de Carrousel";
            var lista = _carouselService.FindAll();
            return View(lista);
        }
    }
}
