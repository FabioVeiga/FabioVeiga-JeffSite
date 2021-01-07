using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JeffSite.Controllers
{
    public class LeitorController : Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LeitorService _leitorService;

        public LeitorController(SocialMidiaService socialMidia, LeitorService leitorService)
        {
            _socialMidia = socialMidia;
            _leitorService = leitorService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Redes = _socialMidia.FindAll();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(){
            return View("Index");
        }
    }
}
