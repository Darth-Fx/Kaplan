namespace Kaplan.Utils.Config.Interfaces
{
    public interface IMailConfig
    {
        string MailCC { get; }
        string MailEmaiForCredential { get; }
        string MailFromAddress { get; }
        string MailJpegContentFilePath { get; }
        string MailPassWordForCredential { get; }
        string MailToAddress { get; }
        string MailSubject { get; }
    }
}