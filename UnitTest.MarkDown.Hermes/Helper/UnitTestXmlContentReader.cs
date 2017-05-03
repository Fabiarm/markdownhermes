using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using MarkDown.Hermes.Helper;
using MarkDown.Hermes.Interfaces;
using NUnit.Framework;

namespace UnitTest.MarkDown.Hermes.Helper
{
    [TestFixture]
    public class UnitTestXmlContentReader : UnitTestHermesBase
    {
        private IXmlContentReader _reader;
        private string _xmlSettingsPathObjFake;
        public override bool UseOptions => true;

        public override void SetUpConfig()
        {
            _reader = new XmlContentReader();
            var assembly = Assembly.GetExecutingAssembly();
            var codeBase = new Uri(assembly.CodeBase);
            _xmlSettingsPathObjFake = Path.Combine(Path.GetDirectoryName(codeBase.LocalPath),
                @"TestData\MarkDown.Hermes.Settings.Fake.xml");
        }

        [Test]
        public void XmlContentReader_GetContent_ShouldReturnNoException()
        {
            var result = _reader.GetContent(string.Empty);
            result.Should().BeEmpty();
        }

        [Test]
        public void XmlContentReader_GetContent_WhenFakeXml_ShouldReturnNoException()
        {
            var result = _reader.GetContent(_xmlSettingsPathObjFake);
            result.Should().BeEmpty();
        }

        [Test]
        public void XmlContentReader_GetTemplateId_ShouldReturnNoException()
        {
            var result = _reader.GetTemplateId(string.Empty);
            result.Should().BeEmpty();
        }

        [Test]
        public void XmlContentReader_GetTemplateId_WhenFakeXml_ShouldReturnNoException()
        {
            var result = _reader.GetTemplateId(_xmlSettingsPathObjFake);
            result.Should().BeEmpty();
        }

        [Test]
        public void XmlContentReader_GetContent_WhenCorrectPath_ShouldReturnNoException()
        {
            var result = _reader.GetContent(XmlSettingsPathObj);
            result.Should().NotBeNullOrEmpty();
            result.Should().Contain("@");
        }

        [Test]
        public void XmlContentReader_GetTemplateId_WhenCorrectPath_ShouldReturnNoException()
        {
            var result = _reader.GetTemplateId(XmlSettingsPathObj);
            result.Should().NotBeNullOrEmpty();
            result.Should().Contain("Sting");
        }

        [Test]
        public void XmlContentReader_GetContent_WhenInvalidPath_ShouldReturnNoException()
        {
            var result = _reader.GetContent("xmlSettingsPathObj");
            result.Should().BeEmpty();
        }

        [Test]
        public void XmlContentReader_GetTemplateId_WhenInvalidPath_ShouldReturnNoException()
        {
            var result = _reader.GetTemplateId("templateId");
            result.Should().BeEmpty();
        }
    }
}
