using JeffSite_WF_472.Models;
using JeffSite_WF_472.Services;
using System.Web.Mvc;

namespace JeffSite.Controllers
{
    public class SocialMidiaController : Controller
    {
        private readonly UserService _userService;
        private readonly SocialMidiaService _socialMidiaService;
        private readonly LeitorService _leitorService;
        private UserLogged _userLogged;

        public SocialMidiaController(UserService userService, SocialMidiaService socialMidiaService, LeitorService leitorService, UserLogged userLogged)
        {
            _userService = userService;
            _socialMidiaService = socialMidiaService;
            _leitorService = leitorService;
            _userLogged = userLogged;
        }

        public ActionResult Index()
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Redes sociais";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var socialMidias = _socialMidiaService.FindAll();
            return View(socialMidias);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Criar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            return View();
        }

        [HttpPost]
        public ActionResult Create(SocialMidia socialMidia)
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            if (ModelState.IsValid)
            {
                _socialMidiaService.Create(socialMidia);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Deletar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var social = _socialMidiaService.FindById(id);
            return View(social);
        }

        [HttpPost]
        public ActionResult Delete(SocialMidia socialMidia)
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            _socialMidiaService.Delete(socialMidia);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Editar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var social = _socialMidiaService.FindById(id);
            return View(social);
        }

        [HttpPost]
        public ActionResult Edit(SocialMidia socialMidia)
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            _socialMidiaService.Edit(socialMidia);
            return RedirectToAction("Index");
        }

    }
}
