﻿using JeffSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JeffSite.Services;
using System.Threading.Tasks;

namespace JeffSite.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserService _userService;

        public AdminController(UserService userService){
            _userService = userService;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidateUser(User userLogged)
        {
            bool validUser = _userService.ValidateUser(userLogged);
            if (validUser)
            {
                HttpContext.Session.SetString("userLogged", userLogged.user);
                return RedirectToAction(nameof(AdminHome));
            }
            TempData["message"] = "Usuario ou Senha invalido!";
            TempData["user"] = userLogged.user;
            HttpContext.Session.SetString("userLogged", "");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("userLogged", "");
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(User user){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(AdminHome));
        }

    }
}
