using JeffSite_WF_472.Models;
using JeffSite_WF_472.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.IO;
using System.Web;
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
        public ActionResult Edit(Configuracao configuracao, HttpPostedFileBase ImgLogo, HttpPostedFileBase ImgProfile)
        {
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            configuracao.ImgLogo = "imgLogo.jpg";
            configuracao.ImgProfile = "imgProfile.jpg";
            var pathImg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "content\\img");

            if (ImgLogo != null){
                if(ImgLogo.FileName.ToLower() != configuracao.ImgLogo.ToLower())
                {
                    ViewBag.FileNameLogoErro = $"O nome do arquivo deve ser {configuracao.ImgLogo}";
                    return View(nameof(Index));
                }
                //altera imagem logo
                var pathImageSiteOriginal = Path.Combine(pathImg, configuracao.ImgLogo);
                var pathImageSiteImgChanged = Path.Combine(pathImg, ImgLogo.FileName);

                System.IO.File.Move(pathImageSiteOriginal, pathImageSiteImgChanged);
                ImgLogo.SaveAs(pathImageSiteImgChanged);
            }

            if(ImgProfile != null){
                if(ImgProfile.FileName != configuracao.ImgProfile){
                    ViewBag.FileNameProfileErro = $"O nome do arquivo deve ser {configuracao.ImgProfile}";
                    return View(nameof(Index));
                }
                //altera imagem profile
                var pathImageProfileSiteOriginal = Path.Combine(pathImg, ImgProfile.FileName);
                var pathImageProfileSiteImgChanged = Path.Combine(pathImg, ImgProfile.FileName);
                System.IO.File.Move(pathImageProfileSiteOriginal, pathImageProfileSiteImgChanged);
                ImgProfile.SaveAs(pathImageProfileSiteImgChanged);
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
