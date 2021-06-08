using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_finance.Infrastructure
{
    public static class Logger
    {
        private static string file = "log.txt";

        public static void Info(string message)
        {
            using (var writer = new StreamWriter(file, true, Encoding.UTF8))
            {
                writer.WriteLine($"{DateTime.Now} | {LogLevel.Info} | {message}");
            }
        }

        public static void Error(string message)
        {
            using (var writer = new StreamWriter(file, true, Encoding.UTF8))
            {
                writer.WriteLine($"{DateTime.Now} | {LogLevel.Error} | {message}");
            }
        }

        public static void Show()
        {
            var fileInfo = new FileInfo(file);

            Process.Start("notepad.exe", fileInfo.FullName);
        }
    }

    public enum LogLevel
    {
        Info = 0,
        Error
    }
}
