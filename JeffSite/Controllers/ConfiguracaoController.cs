using JeffSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JeffSite.Services;
using System.IO;

namespace JeffSite.Controllers
{
    public class ConfiguracaoController : Controller
    {
        private readonly UserService _userService;
        private readonly ConfiguracaoService _configuracaoService;

        public ConfiguracaoController(UserService userService, ConfiguracaoService configuracaoService)
        {
            _userService = userService;
            _configuracaoService = configuracaoService;
        }

        public IActionResult Index()
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Configurações do site";
            var configs = _configuracaoService.Find();
            return View(configs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Configuracao configuracao, IFormFile ImgLogo)
        {
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            if(ImgLogo.FileName != "imgLogo.jpg"){
                ViewBag.FileNameErro = "O nome do arquivo deve ser imgLogo.jpg";
                return View(nameof(Index));
            }

            var pathImageSite = $@"../JeffSite/wwwroot/img/imgLogo.jpg";
            var pathImageSiteImg = $@"../JeffSite/wwwroot/img/{ImgLogo.FileName}";
            System.IO.File.Move(pathImageSite, pathImageSiteImg);

            configuracao.ImgLogo = ImgLogo.FileName;
            _configuracaoService.Edit(configuracao);
            
            using (var stream = new FileStream(pathImageSite, FileMode.Create))
            {
                ImgLogo.CopyTo(stream);
                
            }

            TempData["message"] = "Alterado com sucesso!";
            return RedirectToAction("Index");
        }

    }
}
