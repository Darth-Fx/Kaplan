using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipLib.Interfaces;

namespace Kaplan.Decorators
{
    /// <summary>
    /// Decorator Extension of the ZipManager class for logging before and after execution of the method(s).
    /// </summary>
    public class ZipManagerLogDecorator : IZipManager
    {
        private IZipManager _zipManagerInstance;

        public ZipManagerLogDecorator(IZipManager zipmanager)
        {
            _zipManagerInstance = zipmanager;
        }
        public void ZipAndEncrypt(string startpath, string filenamepath, string pass, List<string> extensionstozip)
        {
            Logger.Instance.Add($"Starting the Zip and encrypt process");
            _zipManagerInstance.ZipAndEncrypt(startpath, filenamepath, pass, extensionstozip);
            Logger.Instance.Add($"Zip and Encrypt was successful");
        }
    }
}
