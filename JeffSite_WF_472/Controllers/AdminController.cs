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
        private UserLogged _userLogged;

        public AdminController(UserService userService, LojaService lojaService, LeitorService leitorService, UserLogged userLogged)
        {
            _userService = userService;
            _lojaService = lojaService;
            _leitorService = leitorService;
            _userLogged = userLogged;
        }

        private bool IsUserLogged()
        {
            if (string.IsNullOrEmpty(_userLogged.UserName))
            {
                return false;
            }
            return true;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdminHome()
        {
            ViewBag.QuantidadeLivros = _lojaService.HowManyLivros();
            ViewBag.QuantidadePedidos = _lojaService.HowManyPedidos();
            ViewBag.QuantidadePostsAprovado = _leitorService.HowManyPostsApproved();
            ViewBag.QuantidadePostsNaoAprovado = _leitorService.HowManyPostsAreNotApproved();
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            return View(nameof(AdminHome));
        }

        [HttpPost]
        public ActionResult ValidateUser(User userLogged)
        {
            bool validUser = _userService.ValidateUser(userLogged);
            if (validUser)
            {
                _userLogged.UserName = userLogged.UserName;
                return RedirectToAction(nameof(AdminHome));
            }
            TempData["message"] = "Usuario ou Senha invalido!";
            TempData["user"] = userLogged.UserName;
            HttpContext.Session["userLogged"] = "";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _userLogged.UserName = string.Empty;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult ChangePassword(){
            if (!IsUserLogged())
                return RedirectToAction(nameof(Index));

            var user = _userService.GetUserBYLogin(_userLogged.UserName);
            user.Pass = string.Empty;
            return View(user);
        }

        [HttpPost]
        public ActionResult ChangePassword(User user){
            if (!IsUserLogged())
                return RedirectToAction(nameof(Index));

            _userService.ChangePassword(user);
            return RedirectToAction(nameof(AdminHome));
        }

    }
}
