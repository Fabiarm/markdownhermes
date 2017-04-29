using System;
using CommandLine;
using MarkDown.Hermes.Interfaces;
using MarkDown.Hermes.Models;

namespace MarkDown.Hermes.Helper
{
    internal static class Executor
    {
        public static void Run(string[] args, IOutputWriter writer)
        {
            try
            {
                if (writer == null)
                    throw new ArgumentNullException(nameof(writer));

                var options = new Options();
                if (Parser.Default.ParseArguments(args, options))
                {
                    Validator.CheckOptions(options, writer);
                    Validator.CheckDirectories(options, writer);
                    var dllPath = Validator.GetDllPath(options);
                    var xmlPath = Validator.GetXmlDocPath(options);
                    var reactor = new MarkDownReactor(writer);
                    reactor.Load(dllPath, xmlPath);
                    reactor.Build(options);
                }
                else
                    throw new Exception($"Please check configuration.");
            }
            catch (Exception ex)
            {
                writer?.WriteError(ex);
            }
        }
    }
}
