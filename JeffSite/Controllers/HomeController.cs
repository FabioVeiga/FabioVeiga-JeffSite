using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JeffSite.Models;
using JeffSite.Services;
using System.Text.RegularExpressions;
using JeffSite.Utils;

namespace JeffSite.Controllers
{
    public class HomeController : Controller
    {

        private readonly UserService _userService;
        private readonly ConfiguracaoService _configuracaoService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserService userService, ConfiguracaoService configuracaoService)
        {
            _logger = logger;
            _userService = userService;
            _configuracaoService = configuracaoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BioCompleta()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SendEmail(string namecontact, string emailcontact, string phonecontact, string subjectcontact){
            bool val = true;
            bool enviado = true;
            // Recuperar o email que está cadastrado nas configs.
            // Email abaixo está cadastrado no MailJet, provedor com 6000 msg/mês gratuitas
            string email = "rika_alves@hotmail.com";  

            // Verifica se o nome foi digitado
            if(string.IsNullOrEmpty(namecontact)){
                ViewBag.ErroNome = "Campo nome não pode ser vazio ou nulo!";
                val = false;
            }
            // Verifica se o email foi digitado
            if(string.IsNullOrEmpty(emailcontact)){
                ViewBag.ErroEmail = "Campo e-mail não pode ser vazio ou nulo!";
                val = false;
            }
            // Caso o email foi digitado, verifica se ele está correto
            else{

                if(!Regex.IsMatch(emailcontact, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)){
                    ViewBag.ErroEmail = "E-mail digitado não está correto!";
                    val = false;
                }
            }
            // Verifica se o numero de telefone foi digitado
            if(string.IsNullOrEmpty(phonecontact)){
                ViewBag.ErroPhone = "Campo telefone não pode ser vazio ou nulo!";
                val = false;
            }
            // Verifica se o assunto foi digitado
            if(string.IsNullOrEmpty(subjectcontact)){
                ViewBag.ErroSubject = "Campo assunto não pode ser vazio ou nulo!";
                val = false;
            }

            // verifica se todas as validações foram feitas.
            if(val){
                
                // Cria Instancia da classe enviar email
                EnviarEmail env_mail = new EnviarEmail();

                // Se passou em todas as validações, realiza o envio de email
                if(env_mail.testeEmail(email, emailcontact, subjectcontact, namecontact, phonecontact)){
                    ViewBag.Message = "Mensagem enviada!";
                    enviado = true;
                };
            // Caso não passe em alguma validação uma mensagem de erro será escrita
            }else{
                ViewBag.Message = "Mensagem não enviada!";
                enviado = false;
            }

            // Se erro, Os campos continuam preenchidos para arrumar ou preencher
            if(!enviado){
                ViewBag.NameContact = namecontact;
                ViewBag.EmailContact = emailcontact;
                ViewBag.PhoneContact = phonecontact;
                ViewBag.SubjectContact = subjectcontact;
            }
            // Se o email for enviado, os campos são zerados para um novo envio
            else{
                ViewBag.NameContact = "";
                ViewBag.EmailContact = "";
                ViewBag.PhoneContact = "";
                ViewBag.SubjectContact = "";
            }
            
            return View("Contato");
            
        }
    }
}
