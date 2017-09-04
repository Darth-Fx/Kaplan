using Kaplan.Config;
using Kaplan.Decorators;
using Kaplan.Interfaces;
using System;
using ZipLib;
using ZipLib.Interfaces;

namespace Kaplan
{
    /// <summary>
    /// Review the app.config before running this program!
    /// </summary>
    class Program
    {
        private static bool procesResult = false;
        static void Main(string[] args)
        {
            IWinServicesManager winServiceManager = new WinServicesManagerTimeOutRetryDecorator(
                new WinServicesManagerLogDecorator(new WinServicesManager(AppConfig.ServicesMachine)));
            IZipManager zipManager = new ZipManagerLogDecorator(new ZipManager());
            IFileManager fileManager = new FileManagerLogDecorator(new FileManager());
            IProcess process = new KaplanProcess(winServiceManager, zipManager, fileManager);

            try
            {
                process.StartProcess();
                procesResult = true;
            }
            catch (Exception ex)
            {
                Logger.Instance.Add("Er is een fout opgetreden in het proces.");
                Logger.Instance.Add(ex.Message + ex.StackTrace);
            }

            try
            {
                process.FinalizeProcess(procesResult);
            }
            catch (Exception ex)
            {
                Logger.Instance.Add("Er is een fout opgetreden bij het afronden van het proces.");
                Logger.Instance.Add(ex.Message + ex.StackTrace);
            }
            finally
            {
                Logger.Instance.Flush();
            }
         }
    } 
}

