using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite.Models.Livro;
using JeffSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JeffSite.Models.Loja;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JeffSite.Controllers
{
    
    
    public class LojaController : Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LivroService _livroService;
        private readonly ConfiguracaoService _configuracaoService;
        private readonly LeitorService _leitorService;
        private const string titlePage = "Loja";
        public LojaController(SocialMidiaService socialMidia, LivroService livroService, ConfiguracaoService configuracaoService,  LeitorService leitorService)
        {
            _socialMidia = socialMidia;
            _livroService = livroService;
            _configuracaoService = configuracaoService;
            _leitorService = leitorService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Redes = _socialMidia.FindAll();
            ViewBag.Title = titlePage;
            ViewBag.LinkMercadoLivre = _configuracaoService.Find().UrlMercadoLivre;

            ICollection<Livro> livros = new List<Livro>();

            foreach (var livro in _livroService.FindAll())
            {
                livro.WhereToBuys = _livroService.FindAllWhereToBuyByIdLivro(livro.Id);
                livros.Add(livro);
            }

            ViewBag.Livros = livros;

            return View();
        }

        public IActionResult ListLivros(){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Livro";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var livros = _livroService.FindAll();
            return View(livros);
        }
        
        [HttpGet]
        public IActionResult Create(){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Novo";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Livro livro, IFormFile Img){
            int proxId = _livroService.FindNextIdLivro();
            livro.ImgName = string.Concat(proxId,"_",Img.FileName);
            string path = Path.Combine("wwwroot","img","Livro",livro.ImgName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                Img.CopyTo(stream); 
            }
            var item = _livroService.Create(livro, proxId);
            return RedirectToAction("CreateWhereToBuy", item);
        }

        [HttpGet]
        public IActionResult Delete(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Deletar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var livro = _livroService.FindById(id);
            var itemsWTB = _livroService.FindAllWhereToBuyByIdLivro(id);
            var itemsPedidos = _livroService.FindAllPedidosByIdLivro(id);
            
            if(itemsWTB.Count > 0 || itemsPedidos.Count > 0){
                ViewBag.flagDelete = false;
                ViewBag.qtdWTB = itemsWTB.Count;
                ViewBag.qtdPedido = itemsPedidos.Count;
            }else{
                ViewBag.flagDelete = true;
            }


            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Livro livro){
            string path = Path.Combine("wwwroot","img","Livro",livro.ImgName);
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            file.Delete();
            _livroService.Delete(livro);
            return RedirectToAction("ListLivros");
        }

        [HttpGet]
        public IActionResult Edit(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Editar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var livro = _livroService.FindById(id);
            
            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Livro livro, IFormFile Img){
            if(Img != null){
                if(Img.FileName != livro.ImgName){
                    string path = Path.Combine("wwwroot","img","Livro",livro.ImgName);
                    System.IO.FileInfo file = new System.IO.FileInfo(path);
                    file.Delete();
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Img.CopyTo(stream); 
                    }
                }
            }
            
            _livroService.Edit(livro);
            return RedirectToAction("ListLivros");
        }

        [HttpGet]
        public IActionResult CreateWhereToBuy(Livro livro){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Adicionar URL de compra";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            livro.WhereToBuys = _livroService.FindAllWhereToBuyByIdLivro(livro.Id);
            return View(livro);
        }

        [Route("AddWhereToBuy")]
        [HttpGet]
        public IActionResult CreateWhereToBuy(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Adicionar URL de compra";
            var livro = _livroService.FindById(id);
            livro.WhereToBuys = _livroService.FindAllWhereToBuyByIdLivro(livro.Id);
            return View(livro);
        }


        [HttpGet]
        public IActionResult EditWhereToBuy(int id, int idLivro){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Editar URL de compra";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindWhereToBuyById(id);
            item.Livro = _livroService.FindById(idLivro);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWhereToBuy(WhereToBuy item){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            item.Livro = _livroService.FindById(item.Livro.Id);
            await _livroService.UpdateWhereToBuyAsync(item);

            return RedirectToAction("CreateWhereToBuy", item.Livro);
        }


        [HttpGet]
        public IActionResult DeleteWhereToBuy(int id, int idLivro){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Deletar URL de compra";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindWhereToBuyById(id);
            item.Livro = _livroService.FindById(idLivro);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWhereToBuy(WhereToBuy item){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            item.Livro = _livroService.FindById(item.Livro.Id);
            await _livroService.DeleteWhereToBuyAsync(item);

            return RedirectToAction("CreateWhereToBuy", item.Livro);
        }

        [HttpGet]
        public IActionResult Pedido(string filtroStatus, int limit = 10){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Pedidos";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            ViewBag.Limit = limit;
            ViewBag.Status = filtroStatus;
            if(!string.IsNullOrEmpty(filtroStatus)){
                Status s =  (Status)Enum.Parse(typeof(Status), filtroStatus);
                var pedidos = _livroService.FindPedidosByStatus(limit,s);
                return View(pedidos);
            }else{
                var pedidos = _livroService.FindAllPedidos(limit);
                return View(pedidos);
            }
        }

        [HttpGet]
        public IActionResult PedidoAddInfo(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Adicionar informação do pedido";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindPedidoById(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PedidoAddInfo(Pedido pedido, string outrosStatus){
            ViewData["Title"] = "Adicionar informação do pedido";
            var livro = _livroService.FindById(pedido.LivroId);
            string emailFrom = _configuracaoService.FindAdminEmail();
            switch((int)pedido.Status)
            {
                case 1:
                    if(ValidarCampo(1, pedido.LinkPagamento)){
                        ViewBag.Erro = "Por favor preencher o campo!";
                        return View(pedido);
                    }else{
                        var configEmail = _configuracaoService.FindEmail();
                        bool envioEmail = JeffSite.Utils.EnviarEmail.testeEmail(
                            configEmail,
                            emailFrom, pedido.Email, string.Concat("Pedido: ", pedido.Id), 
                            pedido.Nome, null, "ModeloPedidoLinkPagamento",livro.Title, pedido.Id, 
                            pedido.LinkPagamento);
                        if(envioEmail){
                            pedido.Status = Status.Aguardando_Pagamento;
                        }else{
                            ViewBag.Erro = "Houve algum erro no email do email, tentar mais tarde!";
                            pedido.Status = Status.Aguardando_Link_De_Pagamento;
                            _livroService.EditPedido(pedido);
                            return View(pedido);
                        }
                    }
                    break;
                case 2:
                    if(outrosStatus == "PagoSim"){
                        pedido.Status = Status.Pago_e_Aguardando_Dedicatorio;
                    }else{
                        pedido.Status = Status.Aguardando_Pagamento;
                    }
                    break;
                case 3:
                    if(outrosStatus == "DedicadoSim"){
                        pedido.Status = Status.Aguardando_Postagem;
                    }else{
                        pedido.Status = Status.Pago_e_Aguardando_Dedicatorio;
                    }
                    break;
                case 4:
                    if(ValidarCampo(4, pedido.LinkRastreio)){
                        ViewBag.Erro = "Por favor preencher o campo!";
                        return View(pedido);
                    }else{
                        var configEmail = _configuracaoService.FindEmail();
                        bool envioEmail = JeffSite.Utils.EnviarEmail.testeEmail(
                            configEmail,
                            emailFrom, pedido.Email, string.Concat("Pedido: ", pedido.Id), 
                            pedido.Nome, null, "ModeloPedidoLinkRastreio",livro.Title, pedido.Id, 
                            pedido.LinkRastreio);
                        if(envioEmail){
                            pedido.Status = Status.Enviado;
                        }else{
                            ViewBag.Erro = "Houve algum erro no email do email, tentar mais tarde!";
                            pedido.Status = Status.Aguardando_Postagem;
                            _livroService.EditPedido(pedido);
                            return View(pedido);
                        }
                        
                    }
                    break;
            }
            
            _livroService.EditPedido(pedido);
            
            ViewBag.Limit = 10;
            var pedidos = _livroService.FindAllPedidos(ViewBag.Limit);

            return RedirectToAction("Pedido",pedidos);
        }

        private bool ValidarCampo(int status, string info){
            int[] statusValido = {1,4};
            if(statusValido.Contains(status) && string.IsNullOrEmpty(info)){
                return true;
            }else{
                return false;
            }
        }

        [HttpGet]
        public IActionResult EditPedido(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Editar Pedido";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindPedidosById(id);
            return View(item);
        }

         [HttpGet]
        public IActionResult DeletePedido(int id){
            var userLogged = HttpContext.Session.GetString("userLogged");
            if (userLogged == "" || userLogged == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewData["Title"] = "Deletar Pedido";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindPedidosById(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePedido(Pedido item){
            _livroService.DeletePedido(item);
            ViewBag.Limit = 10;
            var pedidos = _livroService.FindAllPedidos(ViewBag.Limit);
            return RedirectToAction("Pedido",pedidos);
        }

    }
}
