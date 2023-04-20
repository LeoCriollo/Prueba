using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace DoleEcIntranet.Tools
{
    public static class Mail
    {
        /// <summary>
        /// Send a mail reading from Config file this variables mailMessage.FromAddress, message.IsBodyHtml, smtpClient.Host
        /// </summary>
        /// <param name="message">System.Net.Mail.MailMessage message</param>
        /// <returns>SentMailInfo</returns>
        public static SentMailInfo Send(MailMessage message)
        {
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                // add email address (from address) to blind carbon copy
                //message.Bcc.Add(ConfigurationManager.AppSettings["mailMessage.FromAddress"]);
                // send mail
                smtpClient.Host = ConfigurationManager.AppSettings["smtpClient.Host"];
                smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["smtpClient.Port"]);
                smtpClient.UseDefaultCredentials = bool.Parse(ConfigurationManager.AppSettings["smtpClient.UseDefaultCredentials"]);
                //if (!smtpClient.UseDefaultCredentials)
                //{
                smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpClient.User"], ConfigurationManager.AppSettings["smtpClient.Password"]);
                //}
                smtpClient.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtpClient.EnableSsl"]);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
                smtpClient.Send(message);
                // return mailing information
                return new SentMailInfo(true, "Email enviado exitosamente");
            }
            catch (FormatException fex)
            {
                return new SentMailInfo(false, " La dirección de email pasada como parámetro es inválida: " + fex.Message);
            }
            catch (Exception ex)
            {
                return new SentMailInfo(false, "No se pudo enviar el mensaje: " + ex.Message);
            }
        }
        /// <summary>
        /// Send a mail reading from Config file this variables mailMessage.FromAddress, message.IsBodyHtml, smtpClient.Host
        /// </summary>
        /// <param name="to">string to represents the mail destinatary</param>
        /// <param name="subject">string subject represents the mail subjedt</param>
        /// <param name="body">string subject represents the mail subjedt</param>
        /// <returns>SentMailInfo</returns>
        public static SentMailInfo Send(string to, List<string> cc, string subject, string body)
        {
            MailMessage message = new MailMessage();
            try
            {
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["mailMessage.FromAddress"]/*, "Franki"*/);
                MailAddress toAddress = new MailAddress(to);
                message.From = fromAddress;
                message.To.Add(toAddress); //Recipent email
                foreach (string item in cc)
                    message.CC.Add(new MailAddress(item));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = bool.Parse(ConfigurationManager.AppSettings["mailMessage.IsBodyHtml"]);
                return Send(message);
            }
            catch (FormatException fex)
            {
                return new SentMailInfo(false, " La dirección de email pasada como parámetro es inválida: " + fex.Message);
            }
            catch (Exception ex)
            {
                return new SentMailInfo(false, "No se pudo enviar el mensaje: " + ex.Message);
            }
        }
        /// <summary>
        /// Send a mail with attachments, reading from Config file this variables mailMessage.FromAddress, message.IsBodyHtml, smtpClient.Host
        /// </summary>
        /// <param name="to">string to represents the mail destinatary</param>
        /// <param name="subject">string subject represents the mail subjedt</param>
        /// <param name="body">string subject represents the mail subjedt</param>
        /// <param name="attachments">AttachmentCollection attachments represents the mail attachments files</param>
        /// <returns>SentMailInfo</returns>
        public static SentMailInfo Send(string to, List<string> cc, string subject, string body, List<string> attachmentsPaths)
        {
            MailMessage message = new MailMessage();
            try
            {
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["mailMessage.FromAddress"]);
                MailAddress toAddress = new MailAddress(to);
                message.From = fromAddress;
                message.To.Add(toAddress); //Recipent email
                foreach (string item in cc)
                    message.CC.Add(new MailAddress(item));
                message.Subject = subject;
                message.Bcc.Add(ConfigurationManager.AppSettings["correo_bcc"]);
                message.Body = body;
                message.IsBodyHtml = bool.Parse(ConfigurationManager.AppSettings["mailMessage.IsBodyHtml"]);
                try
                {
                    foreach (string attach in attachmentsPaths)
                        message.Attachments.Add(new Attachment(attach));
                }
                catch (Exception ex)
                {
                    //EventsLogManager.Instance.WriteWarningEntry("Error en hilo de envío de mails de comprobantes, no se pudo adjuntar archivo: " + ex.Message);
                }
                return Send(message);
            }
            catch (FormatException fex)
            {
                return new SentMailInfo(false, " La dirección de email pasada como parámetro es inválida: " + fex.Message);
            }
            catch (Exception ex)
            {
                return new SentMailInfo(false, "No se pudo enviar el mensaje: " + ex.Message);
            }
        }
    }
}