using CommandLine;
using CommandLine.Text;

namespace MarkDown.Hermes.Models
{
    public class Options
    {
        [Option('d', "inputDllFilePath", HelpText = "Input dll file for reflection.", Required = true)]
        public string InputDllFilePath { get; set; }

        [Option('o', "outputDirectoryPath", HelpText = "Output directory where store md files", Required = true)]
        public string OutputDirectoryPath { get; set; }

        [Option('m', "isMiltiFiles", HelpText =
            "Flag, if true: to create separate md file for each class that marked [Documented] attribute, else to generate single file")]
        public bool IsMiltiFiles { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
