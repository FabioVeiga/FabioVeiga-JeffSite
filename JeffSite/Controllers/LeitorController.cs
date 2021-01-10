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
        private const int MaxMegaBytes = 5 * 1024 * 1024;
        private const string titlePage = "Cantinho do leitor";
        private int limitItensView = 9;

        public LeitorController(SocialMidiaService socialMidia, LeitorService leitorService)
        {
            _socialMidia = socialMidia;
            _leitorService = leitorService;
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
        public async Task<IActionResult> Create(Leitor leitor, IFormFile Img){
            ViewBag.Title = titlePage;
            ViewBag.Redes = _socialMidia.FindAll();

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
            }
            ViewBag.Leitores = _leitorService.FindAllApproved(limitItensView);
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
    }
}
