using System;
using System.IO;

namespace DRYDemoLibrary
{
    public class MondLogger : ILogger
    {
        private readonly string _filename = "MondLog.txt";

        public void Log(string message)
        {
            if (!string.IsNullOrWhiteSpace(_filename))
            {
                var file = new StreamWriter(_filename, append: true);
                file.WriteLine($"Mond logger ->{DateTime.Now} - {message}");
                file.Close();
            }
        }
    }
}
