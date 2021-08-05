using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using JeffSite_WF_472.Models;
using JeffSite_WF_472.Models.Livro;
using JeffSite_WF_472.Models.Loja.Api;
using JeffSite_WF_472.Services;


namespace JeffSite_WF_472.Controllers
{
    public class LojaApiController : Controller
    {
        private readonly SocialMidiaService _socialMidia;
        private readonly LivroService _livroService;
        private readonly LojaService _lojaService;
        private readonly MallingService _mallingService;
        private UserLogged _userLogged;

        public LojaApiController(SocialMidiaService socialMidia, LivroService livroService, LojaService lojaService, MallingService mallingService, UserLogged userLogged)
        {
            _socialMidia = socialMidia;
            _livroService = livroService;
            _lojaService = lojaService;
            _mallingService = mallingService;
            _userLogged = userLogged;
        }


        //[HttpPost]
        //public ActionResult Post(ItemWhereToBuy item){
        //
        //    //var model = _livroService.FindAllWhereToBuyByIdLivro(item.LivroId);
        //    //model.
        //    //await _livroService.AddWhereToBuyAsync(item);
        //    return new HttpStatusCodeResult(HttpStatusCode.OK, "Success");
        //}
        //
        //[Route("findlastwheretobuy/{idlivro}")]
        //[HttpGet]
        //public IActionResult FindLastWhereToBuy(int idLivro){
        //    if (!_userLogged.IsUserLogged())
        //        return RedirectToAction("Index", "Admin");
        //
        //    var item = _livroService.FindLastWhereToBuy(idLivro);
        //
        //    return Ok(item);
        //}
        //
        //[Route("add-pedido")]
        //[HttpPost]
        //public IActionResult AddPedido([FromBody]Pedido pedido){
        //    pedido.Id = _lojaService.FindNextIdPedido();
        //    pedido.Status = Status.Aguardando_Link_De_Pagamento;
        //    _lojaService.AddPedido(pedido);
        //
        //    //add email malling
        //    var mail = new Malling();
        //    mail.Email = pedido.Email;
        //    mail.Nome = pedido.Nome;
        //    mail.Onde = "Pedido";
        //    mail.DataCadastro = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
        //
        //    //Add malling
        //    if(!_mallingService.CheckMail(mail)){
        //        _mallingService.AddMalling(mail);
        //    }
        //
        //    return Ok();
        //}

    }
}
