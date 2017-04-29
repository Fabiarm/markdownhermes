using System;
using System.IO;
using System.Text;
using MarkDown.Generator;
using MarkDown.Generator.Helper;
using MarkDown.Hermes.Interfaces;
using MarkDown.Hermes.Models;

namespace MarkDown.Hermes.Helper
{
    internal class MarkDownReactor: IMarkDownReactor
    {
        private readonly MarkDownBuilder _builder;
        private readonly IOutputWriter _writer;

        public MarkDownReactor(IOutputWriter writer)
        {
            _builder = new MarkDownBuilder(SpecHelper.CreateGenerator(), SpecHelper.CreateXmlVsParser());
            _writer = writer;
        }

        public MarkDownReactor()
        {
            _builder = new MarkDownBuilder(SpecHelper.CreateGenerator(), SpecHelper.CreateXmlVsParser());
            _writer = Forge.GetOutputWriter();
        }

        public void Load(string dllPath, string dllXmlPath)
        {
            _builder.Load(dllPath, dllXmlPath);
        }

        public void Build(Options options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            _builder.Build();

            if (_builder.Content == null || _builder.Content.Count <= 0) return;

            _writer.WriteInfo($"Single File mode: {!options.IsMiltiFiles}");

            if (!options.IsMiltiFiles)
            {
                var pathToFile = Path.Combine(options.OutputDirectoryPath, "Home.md");
                File.WriteAllText(pathToFile, _builder.FullContent,
                    Encoding.UTF8);

                _writer.WriteInfo($"File '{pathToFile}' has been created.");
            }
            else
            {
                foreach (var editor in _builder.Content)
                {
                    var pathToFile = Path.Combine(options.OutputDirectoryPath, $"{editor.FileName}.md");
                    File.WriteAllText(pathToFile, editor.ToString(), Encoding.UTF8);
                    _writer.WriteInfo($"File '{pathToFile}' has been created.");
                }
            }
        }
    }
}
