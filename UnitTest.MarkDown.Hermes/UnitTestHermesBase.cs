using System;
using System.IO;
using System.Reflection;
using MarkDown.Hermes.Models;
using NUnit.Framework;

namespace UnitTest.MarkDown.Hermes
{
    [TestFixture]
    public abstract class UnitTestHermesBase
    {
        public abstract bool UseOptions { get; }

        public abstract void SetUpConfig();

        public Options OptionsObj { get; set; }
        public string DllPathObj { get; set; }
        public string XmlPathObj { get; set; }

        public string OutPutPathObj { get; set; }

        [SetUp]
        public void SetUp()
        {
            if (UseOptions)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var codeBase = new Uri(assembly.CodeBase);
                DllPathObj = Path.Combine(Path.GetDirectoryName(codeBase.LocalPath),
                    @"TestData\MarkDown.TestLibrary.dll");
                XmlPathObj = Path.Combine(Path.GetDirectoryName(codeBase.LocalPath),
                    @"TestData\MarkDown.TestLibrary.xml");
                OutPutPathObj = Path.Combine(Path.GetDirectoryName(codeBase.LocalPath),
                    @"Docs");
                OptionsObj = new Options {InputDllFilePath = DllPathObj, OutputDirectoryPath = OutPutPathObj};
            }
            SetUpConfig();
        }
    }
}
