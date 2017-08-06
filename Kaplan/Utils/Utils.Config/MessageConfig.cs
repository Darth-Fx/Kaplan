using Kaplan.Utils.Config.Interfaces;
using System.Configuration;

namespace Kaplan.Utils.Config
{
    public class MessageConfig : IMessageConfig
    {
        public string MessageHtmlTemplatePath
        {
            get { return ConfigurationManager.AppSettings["MessageHtmlTemplatePath"]; }
        }
        public string MessageHtmlHeadTitle
        {
            get { return ConfigurationManager.AppSettings["MessageHtmlHeadTitle"]; }
        }
        public string MessageAanhef
        {
            get { return ConfigurationManager.AppSettings["MessageAanhef"]; }
        }
        public string MessageTitel
        {
            get { return ConfigurationManager.AppSettings["MessageTitel"]; }
        }
        public string MessageSucces
        {
            get { return ConfigurationManager.AppSettings["MessageSucces"]; }
        }
        public string MessageFailure
        {
            get { return ConfigurationManager.AppSettings["MessageFailure"]; }
        }

    }
}
