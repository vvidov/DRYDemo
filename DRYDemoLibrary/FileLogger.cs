using System;
using System.IO;

namespace DRYDemoLibrary
{
    public class FileLogger : ILogger
    {
        private readonly string _filename;

        public FileLogger(string filename = "log.txt")
        {
            _filename = filename;
        }

        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(_filename))
                return;

            try
            {
                using (var writer = new StreamWriter(_filename, append: true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
