using System;
using MarkDown.Hermes.Interfaces;

namespace MarkDown.Hermes.Helper
{
    internal class OutputWriter: IOutputWriter
    {
        public void WriteError(Exception ex)
        {
            if (ex == null) return;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Out.WriteLine($"Exception : {ex.Message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteInfo(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            Console.Out.WriteLine(message);
        }
    }
}
