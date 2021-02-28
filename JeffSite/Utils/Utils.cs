using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using JeffSite.Models;

namespace JeffSite.Utils{

    public static class EnviarEmail{
        
        private static string ModeloEmailContato(string namecontact, string phonecontact, string emailTo, string subject){
            return string.Format(@"<p> Nome: {0}</p>
                 <p>Telefone: {1}</p>
                 <p>Email: {2}</p>
                 <p>Assunto: {3}</p>", namecontact, phonecontact, emailTo, subject); 
        }

        private static string ModeloPedidoLinkPagamento(string nome, string livro, int pedido, string url){
            return string.Format(
                @"<html style=""text-align: center;"" >
                    <table style=""width: 80%;
                    border: 1px solid #B9B9B9;
                    background-color: #E1E1E1;"">
                        <tr>
                        <td>
                            Jeff Autor
                        </td>
                        </tr>
                        <tr>
                        <td style=""text-align: left;"">
                            <p>Pedido: {0}</p>
                            <p>Livro: {1}</p>
                            <p>Nome: {2}</p>
                            <p>Link de Pagamento: <a href=""{3}"">Clique aqui</a></p>
                            <p>Obrigado por comprar nosso livro!</p>
                        </td>
                        </tr>
                    </table>
                    </html>"
                , pedido, livro, nome, url); 
        }

        private static string ModeloPedidoLinkRastreio(string nome, string livro, int pedido, string url){
            return string.Format(
                @"<html style=""text-align: center;"" >
                    <table style=""width: 80%;
                    border: 1px solid #B9B9B9;
                    background-color: #E1E1E1;"">
                        <tr>
                        <td>
                            Jeff Autor
                        </td>
                        </tr>
                        <tr>
                        <td style=""text-align: left;"">
                            <p>Obrigado pelo pagamento.</p><br>
                            <p>Pedido: {0}</p>
                            <p>Livro: {1}</p>
                            <p>Nome: {2}</p>
                            <p>Codigo de rastreio: {3}</a></p>
                            <p>Obrigado por comprar nosso livro!</p>
                        </td>
                        </tr>
                    </table>
                    </html>"
                , pedido, livro, nome, url); 
        }

        public static bool testeEmail(Email config, string emailFrom, string emailTo, string subject, string nome, string phonecontact, string modelo, string livro, int? pedido, string url){
             string texthtml = "";

            switch (modelo)
            {
                case "ModeloEmailContato":
                    texthtml = ModeloEmailContato(nome, phonecontact, emailTo, subject);
                    break;

                case "ModeloPedidoLinkPagamento":
                    texthtml = ModeloPedidoLinkPagamento(nome, livro, pedido.Value, url);
                    break;

                case "ModeloPedidoLinkRastreio":
                    texthtml = ModeloPedidoLinkRastreio(nome, livro, pedido.Value, url);
                    break;
            }
            
                 
            try{
                // Instancia da classe de Mensagem
                MailMessage _mailmessage = new MailMessage();
                // Remetente
                _mailmessage.From = new MailAddress(emailFrom);

            
                // Constroi o MailMessage
                _mailmessage.CC.Add(emailTo);
                _mailmessage.Subject = subject;
                _mailmessage.IsBodyHtml = true;
                _mailmessage.Body = texthtml;
            

                // Configuração com porta
                SmtpClient _smtpClient = new SmtpClient(config.Servidor, config.Porta);

                // Credenciais para o envio por SMTP seguro via MailJet
                _smtpClient.UseDefaultCredentials = config.UsarCredencialPadrao;
                _smtpClient.Credentials = new NetworkCredential(config.ContaEmail,config.Senha);
                _smtpClient.EnableSsl = config.HabilitaSSL;
                _smtpClient.Send(_mailmessage);

                return true;
            }
            catch{
                //TODO: Verificar as exceções que podem vir a calhar.
                return false;
            }
        }

        public static bool enviarEmailMalling(Email config, string emailFrom, List<string> emailTo, string subject, string html){ 
            try{
                // Instancia da classe de Mensagem
                MailMessage _mailmessage = new MailMessage();
                // Remetente
                _mailmessage.From = new MailAddress(emailFrom);

            
                // Constroi o MailMessage
                _mailmessage.Subject = subject;
                foreach (var item in emailTo)
                {
                    _mailmessage.Bcc.Add(item);
                }
                _mailmessage.IsBodyHtml = true;
                _mailmessage.Body = html;
            

                // Configuração com porta
                SmtpClient _smtpClient = new SmtpClient(config.Servidor, config.Porta);

                // Credenciais para o envio por SMTP seguro via MailJet
                _smtpClient.UseDefaultCredentials = config.UsarCredencialPadrao;
                _smtpClient.Credentials = new NetworkCredential(config.ContaEmail,config.Senha);
                _smtpClient.EnableSsl = config.HabilitaSSL;
                _smtpClient.Send(_mailmessage);

                return true;
            }
            catch{
                //TODO: Verificar as exceções que podem vir a calhar.
                return false;
            }
        }
        

    }
    

    
}