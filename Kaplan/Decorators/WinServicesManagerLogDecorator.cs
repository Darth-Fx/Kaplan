using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipLib.Interfaces;

namespace Kaplan.Decorators
{
    /// <summary>
    /// Decorator Extension of the WinServicesManager class for logging before and after execution of the method(s).
    /// </summary>
    public class WinServicesManagerLogDecorator : IWinServicesManager
    {
        private IWinServicesManager _winServiceManager;
        public WinServicesManagerLogDecorator(IWinServicesManager winservicesmanager)
        {
            _winServiceManager = winservicesmanager;
        }
        public void StartService(string serviceName, int timeoutMilliseconds)
        {
            //decorate it with logging
            Logger.Instance.Add($"Try to start the {serviceName} service");
            _winServiceManager.StartService(serviceName, timeoutMilliseconds);
            Logger.Instance.Add($"Started {serviceName} service again");
        }

        public void StopService(string serviceName, int timeoutMilliseconds)
        {
            Logger.Instance.Add($"Try to stop the {serviceName} service");
            _winServiceManager.StopService(serviceName, timeoutMilliseconds);
            Logger.Instance.Add($"Stopped {serviceName} service");
        }

        public Task StopServiceAsync(string serviceName, int timeoutMilliseconds)
        {
            throw new NotImplementedException();
        }
    }
}
