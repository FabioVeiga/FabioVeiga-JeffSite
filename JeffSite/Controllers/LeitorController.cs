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

        public LeitorController(SocialMidiaService socialMidia, LeitorService leitorService)
        {
            _socialMidia = socialMidia;
            _leitorService = leitorService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Redes = _socialMidia.FindAll();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Leitor leitor, IFormFile Img){
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

            return View("Index");
        }
    }
}
