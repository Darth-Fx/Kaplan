using Kaplan.Utils.Config.Interfaces;
using Kaplan.Utils.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Kaplan.Utils
{
    public class Mail : IMail
    {
        private IMailConfig _mailConfig;
        private readonly string SMTP_URL = "smtp.gmail.com";
        private readonly int SMTP_PORT = 587;
        public Mail(IMailConfig mailConfig)
        {
            _mailConfig = mailConfig;
        }
        
        public void SendEmail(string messagebody)

        {
            try
            {
                using (var mail = new MailMessage())
                using (var smtpClient = new SmtpClient(SMTP_URL
                                                    , SMTP_PORT))
                {
                    var loginInfo = new NetworkCredential(_mailConfig.MailEmaiForCredential, _mailConfig.MailPassWordForCredential);
                    if (!string.IsNullOrEmpty(_mailConfig.MailCC))
                    {
                        MailAddress copy = new MailAddress(_mailConfig.MailCC);
                        mail.CC.Add(copy);
                    }
                   mail.From = new MailAddress(_mailConfig.MailFromAddress);
                    mail.To.Add(new MailAddress(_mailConfig.MailToAddress));
                    mail.Subject = _mailConfig.MailSubject;
                    mail.IsBodyHtml = true;

                    //attach a logo/image on top of the messagebody
                    if (!string.IsNullOrEmpty(_mailConfig.MailJpegContentFilePath))
                        mail.AlternateViews.Add(getEmbeddedImage(messagebody, _mailConfig.MailJpegContentFilePath));

                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = loginInfo;
                    smtpClient.EnableSsl = true;    
                    smtpClient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fout bij het versturen van mail bericht.{Environment.NewLine}[Error]{ex.Message}");
            }
        }
        
        /// <summary>
        /// Add a jpeg logo/image above the mail body
        /// https://stackoverflow.com/questions/18358534/send-inline-image-in-email
        /// </summary>
        /// <param name="body"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private AlternateView getEmbeddedImage(string body , String filePath)
        {
            try
            {
                LinkedResource res = new LinkedResource(filePath, MediaTypeNames.Image.Jpeg);
                res.ContentId = Guid.NewGuid().ToString();
                string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>" + body;
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

                alternateView.LinkedResources.Add(res);
                return alternateView;
            }
            catch(Exception ex)
            {
                throw new Exception($"Fout bij het samenstellen van het mailbericht.{Environment.NewLine}[Error]{ex.Message}");
            }
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      