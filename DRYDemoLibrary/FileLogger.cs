using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRYDemoLibrary
{
    public class FileLogger : ILogger
    {
        private readonly string _filename = "Log.txt";

        public void Log(string message)
        {
            if (!string.IsNullOrWhiteSpace(_filename))
            {
                var file = new StreamWriter(_filename, append: true);
                file.WriteLine($"{DateTime.Now} - {message}");
                file.Close();
            }
        }
    }
}
