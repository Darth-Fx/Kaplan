using System.Collections.Generic;

namespace Kaplan.Utils.Interfaces
{
    public interface IHtmlMessage
    {
        string CreateLayout(string messagebody);
        string CreateMessageBody(List<string> linesoftext, bool succes = false);
    }
}