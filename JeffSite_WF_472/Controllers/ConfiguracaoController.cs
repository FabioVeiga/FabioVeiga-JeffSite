using JeffSite_WF_472.Models;
using JeffSite_WF_472.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using System.Web.Mvc;

namespace JeffSite_WF_472.Controllers
{
    public class ConfiguracaoController : Controller
    {
        private readonly UserService _userService;
        private readonly ConfiguracaoService _configuracaoService;
        private readonly LeitorService _leitorService;
        private UserLogged _userLogged;

        public ConfiguracaoController(UserService userService, ConfiguracaoService configuracaoService, LeitorService leitorService, UserLogged userLogged)
        {
            _userService = userService;
            _configuracaoService = configuracaoService;
            _leitorService = leitorService;
            _userLogged = userLogged;
        }

        public ActionResult Index()
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Configurações do site";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var configs = _configuracaoService.Find();
            return View(configs);
        }

        [HttpPost]
        public ActionResult Edit(Configuracao configuracao, FormFile ImgLogo, FormFile ImgProfile)
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            configuracao.ImgLogo = "imgLogo.jpg";
            configuracao.ImgProfile = "imgProfile.jpg";

            if(ImgLogo != null){
                if(ImgLogo.FileName != configuracao.ImgLogo){
                    ViewBag.FileNameLogoErro = $"O nome do arquivo deve ser {configuracao.ImgLogo}";
                    return View(nameof(Index));
                }
                //altera imagem logo
                var pathImageSiteOriginal = $@"../JeffSite/wwwroot/img/{configuracao.ImgLogo}";
                var pathImageSiteImgChanged = $@"../JeffSite/wwwroot/img/{ImgLogo.FileName}";
                System.IO.File.Move(pathImageSiteOriginal, pathImageSiteImgChanged);
                using (var stream = new FileStream(pathImageSiteOriginal, FileMode.Create))
                {
                    ImgLogo.CopyTo(stream); 
                }
            }

            if(ImgProfile != null){
                if(ImgProfile.FileName != configuracao.ImgProfile){
                    ViewBag.FileNameProfileErro = $"O nome do arquivo deve ser {configuracao.ImgProfile}";
                    return View(nameof(Index));
                }
                //altera imagem profile
                var pathImageProfileSiteOriginal = $@"../JeffSite/wwwroot/img/{ImgProfile.FileName}";
                var pathImageProfileSiteImgChanged = $@"../JeffSite/wwwroot/img/{ImgProfile.FileName}";
                System.IO.File.Move(pathImageProfileSiteOriginal, pathImageProfileSiteImgChanged);
                using (var stream = new FileStream(pathImageProfileSiteOriginal, FileMode.Create))
                {
                    ImgProfile.CopyTo(stream); 
                }
            }
            
            _configuracaoService.Edit(configuracao);
            
            TempData["message"] = "Alterado com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult confEmail(){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Configurações do Email";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();

            var item = _configuracaoService.FindEmail();

            return View(item);
        }

        [HttpPost]
        public ActionResult confEmail(Email item){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Configurações do Email";

            if(ModelState.IsValid){
                _configuracaoService.EditEmail(item);
                ViewBag.Message = "Salvo com sucesso!";
            }

            return View(item);
        }

    }
}
