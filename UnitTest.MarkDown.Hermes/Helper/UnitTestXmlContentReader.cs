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
        public override bool UseOptions => true;

        public override void SetUpConfig()
        {
            _reader = new XmlContentReader();
        }
        [Test]
        public void XmlContentReader_GetContent_ShouldReturnNoException()
        {
            string result = _reader.GetContent(string.Empty);
            result.Should().BeEmpty();
        }
        [Test]
        public void XmlContentReader_GetContent_WhenCorrectPath_ShouldReturnNoException()
        {
            string result = _reader.GetContent(XmlSettingsPathObj);
            result.Should().NotBeNullOrEmpty();
            result.Should().Contain("@");
        }
        [Test]
        public void XmlContentReader_GetContent_WhenInvalidPath_ShouldReturnNoException()
        {
            var result = _reader.GetContent("xmlSettingsPathObj");
            result.Should().BeEmpty();
        }
    }
}
