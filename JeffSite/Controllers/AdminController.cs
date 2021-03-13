using JeffSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JeffSite.Services;
using System.Threading.Tasks;

namespace JeffSite.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly LojaService _lojaService;
        private readonly LeitorService _leitorService;

        public AdminController(UserService userService, LojaService lojaService, LeitorService leitorService){
            _userService = userService;
            _lojaService = lojaService;
            _leitorService = leitorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminHome()
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
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
        public IActionResult ValidateUser(User userLogged)
        {
            bool validUser = _userService.ValidateUser(userLogged);
            if (validUser)
            {
                HttpContext.Session.SetString("userLogged", userLogged.UserName);
                return RedirectToAction(nameof(AdminHome));
            }
            TempData["message"] = "Usuario ou Senha invalido!";
            TempData["user"] = userLogged.UserName;
            HttpContext.Session.SetString("userLogged", "");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("userLogged", "");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangePassword(){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            string login = HttpContext.Session.GetString("userLogged");
            User u = _userService.GetUserBYLogin(login);
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(User user){
            _userService.ChangePassword(user);
            return RedirectToAction(nameof(AdminHome));
        }

    }
}
