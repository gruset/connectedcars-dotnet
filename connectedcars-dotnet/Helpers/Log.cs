using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Reflection;

namespace connectedcars_dotnet.Helpers
{
    class Log
    {
        public void LogToConsole(string message)
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logmessage = $"[{ datetime }] > { message }";

            Console.WriteLine(logmessage);
            //LogToFile(message);
        }

        private void LogToFile(string message)
        {
            var CurrentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string logfile = $"{ CurrentDirectory }\\logs\\log_{ DateTime.Now.ToString("yyyy-MM-dd") }.txt";

            using (StreamWriter w = File.AppendText(logfile))
            {
                LogWriter(message, w);
            }
        }

        private void LogWriter(string message, TextWriter w)
        {
            w.Write($"[{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] > { message + Environment.NewLine }");
        }
    }
}
