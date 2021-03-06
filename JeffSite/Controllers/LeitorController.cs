using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Models;
using JeffSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JeffSite.Controllers
{
    public class LeitorController : Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LeitorService _leitorService;
        private readonly MallingService _mallingService;
        private const int MaxMegaBytes = 5 * 1024 * 1024;
        private const string titlePage = "Cantinho do leitor";
        private int limitItensView = 9;

        public LeitorController(SocialMidiaService socialMidia, LeitorService leitorService, MallingService mallingService)
        {
            _socialMidia = socialMidia;
            _leitorService = leitorService;
            _mallingService = mallingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Redes = _socialMidia.FindAll();
            ViewBag.Leitores = _leitorService.FindAllApproved(limitItensView);
            ViewBag.Title = titlePage;
            ViewBag.Limit = limitItensView;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(Leitor leitor, IFormFile Img){
            ViewBag.Title = titlePage;
            ViewBag.Limit = 9;
            ViewBag.Redes = _socialMidia.FindAll();
            ViewBag.Leitores = _leitorService.FindAllApproved(limitItensView);

            if(Img == null){
                ViewBag.ErrorMessage = "Por favor inserir uma imagem!";
                return View("Index");
            }else{
                if(Img.Length >= MaxMegaBytes){
                    ViewBag.ErrorMessage = "Esta imagem é maior que 5Mb!";
                }
            }

            if(ModelState.IsValid){
                DateTime now = DateTime.Now;
                string newNameImg = $@"{now.ToString("yyyyMMddhhmmss")}_{Img.FileName}";
                string path = $@"{leitor.PathImg}{newNameImg}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    Img.CopyTo(stream); 
                }
                leitor.NameImg = newNameImg;
                await _leitorService.CreateAsync(leitor);

                //add email malling
                var mail = new Malling();
                mail.Email = leitor.Email;
                mail.Nome = leitor.Name;

                //Add malling
                if(!_mallingService.CheckMail(mail)){
                    _mallingService.AddMalling(mail);
                }
            }
            
            ViewBag.Send = "Enviado com sucesso!";
            ViewBag.Limit = limitItensView;
            return View("Index");
        }

        [HttpPost]
        public IActionResult MoreItens(int limit)
        {
            limit += 3;
            ViewBag.Redes = _socialMidia.FindAll();
            ViewBag.Leitores = _leitorService.FindAllApproved(limit);
            ViewBag.Title = titlePage;
            ViewBag.Limit = limit;
            return View("Index");
        }

        [Route("ApprovePost")]
        [HttpGet]
        public IActionResult ApprovePost(){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Aprovar Posts";
            var item = _leitorService.FindAllApproved(false);
            return View(item);
        }

        [Route("ApprovePost-id")]
        [HttpGet]
        public IActionResult Approve(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Aprovar este post";
            var item = _leitorService.FindById(id);
            return View(item);
        }

        [Route("ApprovePost")]
        [HttpPost]
        public async Task<IActionResult> ApprovePost(int id){
            await _leitorService.ApprovePostAsync(id);
            return RedirectToAction("ApprovePost");
        }

        [Route("DisapprovePost")]
        [HttpGet]
        public IActionResult Disapprove(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Desaprovar este post";
            var item = _leitorService.FindById(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DisapprovePost(int id){
            var item = _leitorService.FindById(id);
            var pathimg = $@"{item.PathImg}{item.NameImg}";
            System.IO.FileInfo file = new System.IO.FileInfo(pathimg);
            try{
                file.Delete();
                _leitorService.DisapprovePostAsync(item);
            }catch(System.IO.IOException e){
                throw new System.Exception(e.Message);
            }
            
            return RedirectToAction("ApprovePost");
        }
    }
}
