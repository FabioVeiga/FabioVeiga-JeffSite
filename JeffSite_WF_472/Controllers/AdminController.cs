using JeffSite_WF_472.Models;
using JeffSite_WF_472.Services;
using System.Web.Mvc;

namespace JeffSite_WF_472.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly LojaService _lojaService;
        private readonly LeitorService _leitorService;

        public AdminController(UserService userService, LojaService lojaService, LeitorService leitorService)
        {
            _userService = userService;
            _lojaService = lojaService;
            _leitorService = leitorService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminHome()
        {
            var userLogged = Session["userLogged"].ToString();
            if (userLogged == "" || userLogged == null)
            {
                return View(nameof(AdminHome));
            }
            ViewBag.QuantidadeLivros = _lojaService.HowManyLivros();
            ViewBag.QuantidadePedidos = _lojaService.HowManyPedidos();
            ViewBag.QuantidadePostsAprovado = _leitorService.HowManyPostsApproved();
            ViewBag.QuantidadePostsNaoAprovado = _leitorService.HowManyPostsAreNotApproved();
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateUser(User userLogged)
        {
            bool validUser = _userService.ValidateUser(userLogged);
            if (validUser)
            {
                HttpContext.Session["userLogged"] = userLogged.UserName;
                return RedirectToAction(nameof(AdminHome));
            }
            TempData["message"] = "Usuario ou Senha invalido!";
            TempData["user"] = userLogged.UserName;
            HttpContext.Session["userLogged"] = "";
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Logout()
        {
            var userLogged = Session["userLogged"].ToString();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ChangePassword(){
            var userLogged = Session["userLogged"].ToString();
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            string login = Session["userLogged"].ToString();
            User u = _userService.GetUserBYLogin(login);
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(User user){
            _userService.ChangePassword(user);
            return RedirectToAction(nameof(AdminHome));
        }

    }
}
