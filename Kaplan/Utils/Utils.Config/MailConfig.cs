using Kaplan.Utils.Config.Interfaces;
using System.Configuration;

namespace Kaplan.Utils.Config
{
    public class MailConfig :IMailConfig
    {
        public string MailEmaiForCredential
        {
            get { return ConfigurationManager.AppSettings["MailEmaiForCredential"]; }
        }
        public string MailPassWordForCredential
        {
            get { return ConfigurationManager.AppSettings["MailPassWordForCredential"]; }
        }
        
        public string MailFromAddress
        {
            get { return ConfigurationManager.AppSettings["MailFromAddress"]; }
        }
        public string MailToAddress
        {
            get { return ConfigurationManager.AppSettings["MailToAddress"]; }
        }
        public string MailCC
        {
            get { return ConfigurationManager.AppSettings["MailCC"]; }
        }
        public string MailJpegContentFilePath
        {
            get { return ConfigurationManager.AppSettings["MailJpegContentFilePath"]; }
        }

        public string MailSubject => ConfigurationManager.AppSettings["MailSubject"];
    }
}
