using JeffSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JeffSite.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JeffSite.Controllers
{
    public class SocialMidiaController : Controller
    {
        private readonly UserService _userService;
        private readonly SocialMidiaService _socialMidiaService;
        private readonly LeitorService _leitorService;

        public SocialMidiaController(UserService userService, SocialMidiaService socialMidiaService, LeitorService leitorService)
        {
            _userService = userService;
            _socialMidiaService = socialMidiaService;
            _leitorService = leitorService;
        }

        public IActionResult Index()
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Redes sociais";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var socialMidias = _socialMidiaService.FindAll();
            return View(socialMidias);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Criar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SocialMidia socialMidia)
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (ModelState.IsValid)
            {
                _socialMidiaService.Create(socialMidia);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Deletar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var social = _socialMidiaService.FindById(id);
            return View(social);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(SocialMidia socialMidia)
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            _socialMidiaService.Delete(socialMidia);
            return RedirectToAction("Index");
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
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var social = _socialMidiaService.FindById(id);
            return View(social);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SocialMidia socialMidia)
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            _socialMidiaService.Edit(socialMidia);
            return RedirectToAction("Index");
        }

    }
}
