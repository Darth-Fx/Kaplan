
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Kaplan
{
    /// <summary>
    /// Singleton logger
    /// </summary>
    public sealed class Logger
    {
        private static readonly object _padlock = new object();
        private static Logger _instance = null;
        public static Logger Instance
        {
            get
            {
                lock (_padlock)
                {
                    return _instance ?? (_instance = new Logger());
                }
            }
        }
        private Logger()
        { }

        public  List<string> LogList { get; set; }
        public void Add(string message)
        {
            if (LogList == null)
                LogList = new List<string>();
            LogList.Add(message);
        }
        public void Flush()
        {
            // Environment.Current fails if its executed in the scope of the Win Scheduler! Using reflection instead.
            var dir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Log_Information");
           
            if (!Directory.Exists(dir))
                dir = Directory.CreateDirectory(dir).FullName;

            var logFilePath = dir + $@"\ZipManagerLog_{DateTime.Now.ToString("yyyy_dd_M-HH_mm_ss")}.txt";
            
            using (StreamWriter outputFile = new StreamWriter(logFilePath))
            {
                LogList.ForEach((line) => outputFile.WriteLine(DateTime.Now.ToString("yyyy_dd_M-HH_mm_ss") + " : " + line));
            }
        }
    }
}
