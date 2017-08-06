using Kaplan.Utils.Config.Interfaces;
using Kaplan.Utils.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kaplan.Utils
{
    public class HtmlMessage : IHtmlMessage
    {
        private IMessageConfig _messageConfig;
        
        public HtmlMessage(IMessageConfig messageconfig)
        {
            _messageConfig = messageconfig;
        }
        public string CreateMessageBody(List<string> linesoftext, bool succes = false)
        {
            var messagebody = new StringBuilder();
            messagebody = (succes) ? messagebody.AppendLine(_messageConfig.MessageSucces) : messagebody.AppendLine(_messageConfig.MessageFailure);
            
            if (linesoftext != null && linesoftext.Count > 0)
            {
                messagebody.Append("<h4 style = 'color:#000000'>");
                linesoftext.ForEach(line => messagebody.AppendLine(line + "<br />"));
                messagebody.Append("</h4>");
            }
            return messagebody.ToString();
        }

        public string CreateLayout(string messagebody)
        {
            string layout = string.Empty;
            using (StreamReader reader = new StreamReader(_messageConfig.MessageHtmlTemplatePath))
            {
                layout = reader.ReadToEnd();
            }
            layout = layout.Replace("{headtitle}", _messageConfig.MessageHtmlHeadTitle);
            layout = layout.Replace("{UserName}", _messageConfig.MessageAanhef);
            layout = layout.Replace("{Title}", _messageConfig.MessageTitel);
            layout = layout.Replace("{message}", messagebody);
            return layout;
        }
    }
}
