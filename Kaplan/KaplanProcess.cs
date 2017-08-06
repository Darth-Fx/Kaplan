using Kaplan.Config;
using Kaplan.Interfaces;
using Kaplan.Utils;
using Kaplan.Utils.Config;
using Kaplan.Utils.Config.Interfaces;
using System;
using System.Linq;
using ZipLib.Interfaces;

namespace Kaplan
{
    /// <summary>
    /// Specifiek process model voor Kaplan consulting.
    /// Het doel van het proces is het zippen en encrypten van SqlServer database files (mdf/ldf) in een source-directory en het zipbestand
    /// weg te schrijven naar een target-directory. Tijdens het zippen dient de sql service gestopt te zijn.
    /// Het proces eindigt met het opmaken van een rapport (htmlfile) , dat via mail verstuurd wordt.
    /// Alle noodzakelijke informatie dient aanwezig te zijn in de app.config file.
    /// </summary>
    public class KaplanProcess : IProcess
    {
        //
        private IWinServicesManager _winServiceManager;
        private IZipManager _zipManager;
        private IFileManager _fileManager;
        
        public KaplanProcess(IWinServicesManager winservicemanager, IZipManager zipmanager, IFileManager filemanager)
        {
            _winServiceManager = winservicemanager;
            _zipManager = zipmanager;
            _fileManager = filemanager;
        }

        public void StartProcess()
        {
            
            try
            {
                DeleteAllFilesInDir();
                StopServices();
                ZipAndEncrypt();
                StartServices();
            }
            catch (Exception ex)
            {
                throw new Exception($"Er is iets misgegaan in het proces." +
                    $"{Environment.NewLine} [error: {ex.Message}]");
            }
        }
        public void FinalizeProcess(bool result)
        {

            try
            {
                CreateMessageAndMail(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Er is iets misgegaan in het afrondingsproces." +
                    $"{Environment.NewLine} [error: {ex.Message}]");
            }
        }
        private void DeleteAllFilesInDir()
        {
            try
            {
                _fileManager.DeleteAllFilesInDir(AppConfig.DirectoryToDeleteFiles);
            }
            catch (Exception ex)
            {
                throw new Exception($"Er is iets misgegaan bij het verwijderen van de zip files." +
                    $"{Environment.NewLine} [error: {ex.Message}]");
            }
        }
        private void StartServices()
        {
            try
            {
                if (AppConfig.ServicesArray.Length > 0)
                {
                    AppConfig.ServicesArray.ToList().ForEach(item =>
                    {
                        _winServiceManager.StartService(item, AppConfig.TimeOut);
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Er is iets misgegaan bij het opnieuw starten van de services." +
                    $"{Environment.NewLine} [error: {ex.Message}]");
            }
        }

        private void StopServices()
        {
            try
            {
                if (AppConfig.ServicesArray.Length > 0)
                    AppConfig.ServicesArray.ToList().ForEach(item =>
                                                    {
                                                        _winServiceManager.StopService(item, AppConfig.TimeOut);
                                                    }
                                                );
            }
            catch(Exception ex)
            {
                throw new Exception($"Er is iets misgegaan bij het stoppen van de services." +
                    $"{Environment.NewLine} [error: {ex.Message}]");
            }
        }

        private void ZipAndEncrypt()
        {
            try
            {
                _zipManager.ZipAndEncrypt(AppConfig.SourcePath, AppConfig.TargetPath, AppConfig.ZipPass, AppConfig.ExtensionsToZip.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception($"Er is iets misgegaan bij het zippen en encrypten van de files." +
                    $"{Environment.NewLine} [error: {ex.Message}]");
            }
        }

        public void CreateMessageAndMail(bool succes)
        {
            try
            {
                var messagebody = CreateMessage(succes);
                IMailConfig mailconfig = new MailConfig();
                Mail mail = new Mail(mailconfig);
                mail.SendEmail(messagebody);
            }
            catch(Exception ex)
            {
                throw new Exception($"Fout bij het versturen van mail bericht.{Environment.NewLine}[Error]{ex.Message}");
            }
        }

        private string CreateMessage(bool succes)
        {
            try
            {
                IMessageConfig messageconfig = new MessageConfig();
                HtmlMessage htmlmessage = new HtmlMessage(messageconfig);
                var messageBody =  htmlmessage.CreateMessageBody(Logger.Instance.LogList, succes);
                return htmlmessage.CreateLayout(messageBody);
            }
            catch (Exception ex)
            {
                throw new Exception($"Fout bij het aanmaken van mail bericht.{Environment.NewLine}[Error]{ex.Message}");
            }
        }
    } 
}
