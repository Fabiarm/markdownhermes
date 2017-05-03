using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using MarkDown.Hermes.Helper;
using MarkDown.Hermes.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTest.MarkDown.Hermes.Helper
{
    [TestFixture]
    public class UnitTestMarkDownReactor: UnitTestHermesBase
    {
        private MarkDownReactor _reactor;
        private Mock<IOutputWriter> _writerMock;
        private Mock<IXmlContentReader> _xmlReaderMock;
        public override bool UseOptions => true;
        public override void SetUpConfig()
        {
            _writerMock = new Mock<IOutputWriter>();
            _xmlReaderMock = new Mock<IXmlContentReader>();
            _xmlReaderMock.Setup(s => s.GetContent(It.IsAny<string>())).Returns(string.Empty);
            _reactor = new MarkDownReactor(_writerMock.Object, _xmlReaderMock.Object);
        }

        [Test]
        public void MarkDownReactor_WhenDefaultState_Should_ReturnValidResult()
        {
            _reactor.Should().NotBeNull();
        }

        [Test]
        public void MarkDownReactor_Should_ReturnValidResult()
        {
            _reactor = new MarkDownReactor();
            _reactor.Should().NotBeNull();
        }

        [Test]
        [Category("Integration")]
        public void MarkDownReactor_Load_Should_ReturnValidResult()
        {
            _reactor = new MarkDownReactor(Forge.GetOutputWriter(), Forge.GetXmlContentReader());
            _reactor.Load(DllPathObj, XmlPathObj);
        }

        [Test]
        [Category("Integration")]
        public void MarkDownReactor_Build_Should_ReturnValidResult()
        {
            _reactor = new MarkDownReactor(Forge.GetOutputWriter(), Forge.GetXmlContentReader());
            _reactor.Load(DllPathObj, XmlPathObj);
            _reactor.Build(OptionsObj);
        }

        [Test]
        [Category("Integration")]
        public void MarkDownReactor_Build_WhenSingleFile_Should_ReturnFiles()
        {
            OptionsObj.IsMiltiFiles = false;
            OptionsObj.InputSettingsFilePath = null;
            _reactor = new MarkDownReactor(Forge.GetOutputWriter(), Forge.GetXmlContentReader());
            _reactor.Load(DllPathObj, XmlPathObj);
            _reactor.Build(OptionsObj);
            var files = Directory.EnumerateFiles(OutPutPathObj).ToList();
            files.Should().NotBeNull();
            files.Count(s => s.Contains("Home.md")).Should().Be(1);
        }

        [Test]
        [Category("Integration")]
        public void MarkDownReactor_Build_WhenSingleFileAndSettings_Should_ReturnFiles()
        {
            OptionsObj.IsMiltiFiles = true;
            _reactor = new MarkDownReactor(Forge.GetOutputWriter(), Forge.GetXmlContentReader());
            _reactor.Load(DllPathObj, XmlPathObj);
            _reactor.Build(OptionsObj);
            var files = Directory.EnumerateFiles(OutPutPathObj).ToList();
            files.Should().NotBeNull();
            files.Count(s => s.Contains("Home.md")).Should().Be(1);
        }

        [Test]
        [Category("Integration")]
        public void MarkDownReactor_Build_WhenMiltiFiles_Should_ReturnFiles()
        {
            OptionsObj.IsMiltiFiles = true;
            _reactor = new MarkDownReactor(Forge.GetOutputWriter(), Forge.GetXmlContentReader());
            _reactor.Load(DllPathObj, XmlPathObj);
            _reactor.Build(OptionsObj);
            var files = Directory.EnumerateFiles(OutPutPathObj).ToList();
            files.Should().NotBeNull();
            if (OptionsObj.IsMiltiFiles)
                files.Count(s => s.Contains("ITestInterface.md") || s.Contains("TestEnum.md") ||
                                 s.Contains("TestClass.md"))
                    .Should()
                    .Be(3);
        }

        [Test]
        public void MarkDownReactor_WhenNullableOptions_Should_ReturnValidResult()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => _reactor.Build(null));
        }
    }
}
