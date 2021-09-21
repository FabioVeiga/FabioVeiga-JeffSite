using System.IO;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using JeffSite_WF_472.Services;
using JeffSite_WF_472.Models;
using System.Web;

namespace JeffSite_WF_472.Controllers
{
    public class LeitorController : Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LeitorService _leitorService;
        private readonly MallingService _mallingService;
        private const int MaxMegaBytes = 5 * 1024 * 1024;
        private const string titlePage = "Cantinho do leitor";
        private int limitItensView = 9;
        private UserLogged _userLogged;
        private string pathRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
        private string pathLeitor = Path.Combine("Content", "img", "Leitor");

        public LeitorController(SocialMidiaService socialMidia, LeitorService leitorService, MallingService mallingService, UserLogged userLogged)
        {
            _socialMidia = socialMidia;
            _leitorService = leitorService;
            _mallingService = mallingService;
            _userLogged = userLogged;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Redes = _socialMidia.FindAll();
            ViewBag.Leitores = _leitorService.FindAllApproved(limitItensView);
            ViewBag.Title = titlePage;
            ViewBag.Limit = limitItensView;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(Leitor leitor, HttpPostedFileBase PathImg)
        {
            ViewBag.Title = titlePage;
            ViewBag.Limit = 9;
            ViewBag.Redes = _socialMidia.FindAll();
            ViewBag.Leitores = _leitorService.FindAllApproved(limitItensView);

            if(PathImg == null){
                ViewBag.ErrorMessage = "Por favor inserir uma imagem!";
                return View("Index");
            }else{
                if(PathImg.ContentLength >= MaxMegaBytes){
                    ViewBag.ErrorMessage = "Esta imagem é maior que 5Mb!";
                }
            }

            if(ModelState.IsValid){
                DateTime now = DateTime.Now;
                string newNameImg = $@"{now.ToString("yyyyMMddhhmmss")}_{PathImg.FileName}";
                pathRoot += Path.Combine(pathLeitor, newNameImg);
                PathImg.SaveAs(pathRoot);
                leitor.NameImg = newNameImg;
                await _leitorService.CreateAsync(leitor);

                //add email malling
                var mail = new Malling();
                mail.Email = leitor.Email;
                mail.Nome = leitor.Name;
                mail.DataAniversario = leitor.Birthday;
                mail.Onde = "Leitor";
                mail.DataCadastro = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

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
        public ActionResult MoreItens(int limit)
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
        public ActionResult ApprovePost(){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Aprovar Posts";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _leitorService.FindAllApproved(false);
            return View(item);
        }

        [Route("ApprovePost-id")]
        [HttpGet]
        public ActionResult Approve(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Aprovar este post";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _leitorService.FindById(id);
            return View(item);
        }

        [Route("ApprovePost")]
        [HttpPost]
        public async Task<ActionResult> ApprovePost(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            await _leitorService.ApprovePostAsync(id);
            return RedirectToAction("ApprovePost");
        }

        [Route("DisapprovePost")]
        [HttpGet]
        public ActionResult Disapprove(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Desaprovar este post";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _leitorService.FindById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult DisapprovePost(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            var item = _leitorService.FindById(id);
            pathRoot += Path.Combine(pathLeitor, item.NameImg);
            FileInfo file = new FileInfo(pathRoot);
            try{
                file.Delete();
                _leitorService.DisapprovePost(item);
            }catch(System.IO.IOException e){
                throw new System.Exception(e.Message);
            }
            
            return RedirectToAction("ApprovePost");
        }
    }
}
