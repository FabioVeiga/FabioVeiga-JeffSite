using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace JeffSite.Utils{

    public class EnviarEmail{
        public bool testeEmail(string emailFrom, string emailTo, string subject, string namecontact, string phonecontact){
            string texthtml = string.Format(@"<p> Nome: {0}</p>
                 <p>Telefone: {1}</p>
                 <p>Email: {2}</p>
                 <p>Assunto: {3}</p>", namecontact, phonecontact, emailTo, subject); 
                 
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
                SmtpClient _smtpClient = new SmtpClient("in-v3.mailjet.com", Convert.ToInt32("587"));

                // Credenciais para o envio por SMTP seguro via MailJet
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("82e27f3e2c23efde495f3d945230903e","0291dad12068416508fa09631f8c7e2e");
                _smtpClient.EnableSsl = true;
                _smtpClient.Send(_mailmessage);

                return true;
            }
            catch(Exception ex){
                //TODO: Verificar as exceções que podem vir a calhar.
                throw ex;
            }
        }
    }

    
}