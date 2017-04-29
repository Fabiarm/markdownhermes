using System;
using System.IO;
using System.Reflection;
using MarkDown.Hermes.Interfaces;
using MarkDown.Hermes.Models;

namespace MarkDown.Hermes.Helper
{
    internal static class Validator
    {
        public static void CheckOptions(Options options, IOutputWriter writer)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (string.IsNullOrWhiteSpace(options.OutputDirectoryPath) ||
                string.IsNullOrWhiteSpace(options.InputDllFilePath))
            {
                writer.WriteInfo(options.GetUsage());
                return;
            }

            if (!IsFileExist(options.InputDllFilePath))
                throw new FileNotFoundException($"File '{options.InputDllFilePath}' does not exist.");
        }

        public static string GetXmlDocPath(Options options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var dllPath = Path.GetFullPath(options.InputDllFilePath);
            var assembly = Assembly.LoadFile(dllPath);
            var codeBase = new Uri(assembly.CodeBase);
            var xmlPath = Path.Combine(Path.GetDirectoryName(codeBase.LocalPath),
                $"{assembly.GetName().Name}.xml");
            if (!IsFileExist(xmlPath))
                throw new FileNotFoundException($"XML documentation '{xmlPath}' is missing");
            return xmlPath;
        }

        public static string GetDllPath(Options options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return Path.GetFullPath(options.InputDllFilePath);
        }

        public static void CheckDirectories(Options options, IOutputWriter writer)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (Directory.Exists(options.OutputDirectoryPath)) return;
            Directory.CreateDirectory(options.OutputDirectoryPath);
            writer.WriteInfo($"The directory '{options.OutputDirectoryPath}' has been created.");
        }

        public static bool IsFileExist(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            return File.Exists(path);
        }
    }
}
