using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JeffSite_WF_472.Services;
using JeffSite_WF_472.Models.Livro;
using JeffSite_WF_472.Models.Loja;
using System.Web.Mvc;
using JeffSite_WF_472.Models;
using System.Web;

namespace JeffSite_WF_472.Controllers
{


    public class LojaController : Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LivroService _livroService;
        private readonly ConfiguracaoService _configuracaoService;
        private readonly LeitorService _leitorService;
        private const string titlePage = "Loja";
        private UserLogged _userLogged;
        private string pathRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
        private string pathLivro = Path.Combine("Content", "img", "Livro");

        public LojaController(SocialMidiaService socialMidia, LivroService livroService, ConfiguracaoService configuracaoService,  LeitorService leitorService, UserLogged userLogged)
        {
            _socialMidia = socialMidia;
            _livroService = livroService;
            _configuracaoService = configuracaoService;
            _leitorService = leitorService;
            _userLogged = userLogged;
        }
        // GET: /<controller>/
        public ActionResult Index()
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

        public ActionResult ListLivros(){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Livro";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var livros = _livroService.FindAll();
            return View(livros);
        }
        
        [HttpGet]
        public ActionResult Create(){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Novo";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Livro livro, HttpPostedFileBase ImgName){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            int proxId = _livroService.FindNextIdLivro();
            livro.ImgName = string.Concat(proxId,"_",ImgName.FileName);
            pathRoot += Path.Combine(pathLivro, livro.ImgName);
            ImgName.SaveAs(pathRoot);
            var item = _livroService.Create(livro, proxId);
            return RedirectToAction("CreateWhereToBuy", item);
        }

        [HttpGet]
        public ActionResult Delete(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

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
        public ActionResult Delete(Livro livro){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            pathRoot += Path.Combine(pathLivro, livro.ImgName);
            FileInfo file = new FileInfo(pathRoot);
            file.Delete();
            _livroService.Delete(livro);
            return RedirectToAction("ListLivros");
        }

        [HttpGet]
        public ActionResult Edit(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Editar";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var livro = _livroService.FindById(id);
            
            return View(livro);
        }

        [HttpPost]
        public ActionResult Edit(Livro livro, HttpPostedFileBase ImgName){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            var item = _livroService.FindById(livro.Id);
            item.Title = livro.Title;
            item.Description = livro.Description;
            item.Esgotado = livro.Esgotado;
            if (ImgName != null){
                if(ImgName.FileName != item.ImgName){
                    var path = Path.Combine(pathRoot,pathLivro,item.ImgName);
                    FileInfo file = new FileInfo(path);
                    file.Delete();
                    item.ImgName = string.Concat(livro.Id,"_", ImgName.FileName);
                    pathRoot += Path.Combine(pathLivro, item.ImgName);
                    ImgName.SaveAs(pathRoot);
                }
            }
            else
            {
                item.ImgName = string.Concat(livro.Id,"_",item.ImgName);
            }
            
            _livroService.Edit(item);
            return RedirectToAction("ListLivros");
        }

        [HttpGet]
        public ActionResult CreateWhereToBuy(Livro livro){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Adicionar URL de compra";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            livro.WhereToBuys = _livroService.FindAllWhereToBuyByIdLivro(livro.Id);
            return View(livro);
        }

        [Route("AddWhereToBuy")]
        [HttpGet]
        public ActionResult CreateWhereToBuy(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Adicionar URL de compra";
            var livro = _livroService.FindById(id);
            livro.WhereToBuys = _livroService.FindAllWhereToBuyByIdLivro(livro.Id);
            return View(livro);
        }


        [HttpGet]
        public ActionResult EditWhereToBuy(int id, int idLivro){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Editar URL de compra";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindWhereToBuyById(id);
            item.Livro = _livroService.FindById(idLivro);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditWhereToBuy(WhereToBuy item){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            item.Livro = _livroService.FindById(item.Livro.Id);
            await _livroService.UpdateWhereToBuyAsync(item);

            return RedirectToAction("CreateWhereToBuy", item.Livro);
        }


        [HttpGet]
        public ActionResult DeleteWhereToBuy(int id, int idLivro){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Deletar URL de compra";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindWhereToBuyById(id);
            item.Livro = _livroService.FindById(idLivro);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteWhereToBuy(WhereToBuy item){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            item.Livro = _livroService.FindById(item.Livro.Id);
            await _livroService.DeleteWhereToBuyAsync(item);

            return RedirectToAction("CreateWhereToBuy", item.Livro);
        }

        [HttpGet]
        public ActionResult Pedido(string filtroStatus, int limit = 10){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

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
        public ActionResult PedidoAddInfo(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Adicionar informação do pedido";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindPedidoById(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PedidoAddInfo(Pedido pedido, string outrosStatus){
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
                        var configsite = _configuracaoService.Find();
                        bool envioEmail = Utils.EnviarEmail.testeEmail(
                            configEmail,
                            emailFrom, pedido.Email, string.Concat("Pedido: ", pedido.Id), 
                            pedido.Nome, null, "ModeloPedidoLinkPagamento",livro.Title, pedido.Id, 
                            pedido.LinkPagamento, configsite.NomeSite);
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
                        var configSite = _configuracaoService.Find();
                        bool envioEmail = Utils.EnviarEmail.testeEmail(
                            configEmail,
                            emailFrom, pedido.Email, string.Concat("Pedido: ", pedido.Id), 
                            pedido.Nome, null, "ModeloPedidoLinkRastreio",livro.Title, pedido.Id, 
                            pedido.LinkRastreio, configSite.NomeSite);
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
        public ActionResult EditPedido(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Editar Pedido";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindPedidosById(id);
            return View(item);
        }

         [HttpGet]
        public ActionResult DeletePedido(int id){
            if (!_userLogged.IsUserLogged())
                return RedirectToAction("Index", "Admin");

            ViewData["Title"] = "Deletar Pedido";
            ViewBag.QuantidadeDeAprovacao = _leitorService.HowManyPostsAreNotApproved();
            var item = _livroService.FindPedidosById(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePedido(Pedido item){
            _livroService.DeletePedido(item);
            ViewBag.Limit = 10;
            var pedidos = _livroService.FindAllPedidos(ViewBag.Limit);
            return RedirectToAction("Pedido",pedidos);
        }

    }
}
