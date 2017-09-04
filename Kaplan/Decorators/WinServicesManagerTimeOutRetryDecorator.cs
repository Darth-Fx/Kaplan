using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZipLib.Interfaces;

namespace Kaplan.Decorators
{
    public class WinServicesManagerTimeOutRetryDecorator : IWinServicesManager
    {
        private IWinServicesManager _winServiceManager;
        public WinServicesManagerTimeOutRetryDecorator(IWinServicesManager winservicesmanager)
        {
            _winServiceManager = winservicesmanager;
        }
        public void StartService(string serviceName, int timeoutMilliseconds)
        {
            _winServiceManager.StartService(serviceName, timeoutMilliseconds);
        }

        public void StopService(string serviceName, int timeoutMilliseconds)
        {
            this.StopService(serviceName, timeoutMilliseconds, 5);
        }

        private void StopService(string serviceName, int timeoutMilliseconds, int count)
        {
            try
            {
                _winServiceManager.StopService(serviceName, timeoutMilliseconds);
            }
            catch (Exception ex)
            {
                if (count <= 0 || !IsTimeOutException(ex))
                    throw;

                Thread.Sleep(300);
                Logger.Instance.Add($"Trying to Stop the service again. {count} tries to go...");
                this.StopService(serviceName, timeoutMilliseconds, count - 1);
            }
        }

        private static bool IsTimeOutException(Exception ex)
        {
            while (ex != null)
            {
                if (ex is Exception && ex.Message.Contains("expired"))
                    return true;

                ex = ex.InnerException;
            }

            return false;
        }
        public Task StopServiceAsync(string serviceName, int timeoutMilliseconds)
        {
            throw new NotImplementedException();
        }
    }
}
