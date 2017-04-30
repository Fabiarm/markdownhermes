using System;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTest.MarkDown.Generator
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class UnitTestXmlVsParser: UnitTestMdBase
    {
        /// <inheritdoc />
        public override bool UseTypes => false;

        /// <inheritdoc />
        public override bool UseComments => false;

        /// <inheritdoc />
        public override bool UseMarkDownGenerator => false;

        /// <inheritdoc />
        public override bool UseXmlVsParser => true;

        /// <inheritdoc />
        public override bool UseTestExecutingAssembly => true;

        /// <inheritdoc />
        public override void SetUpConfig()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void XmlVsParser_Should_ReturnValidResult()
        {
            var list = XmlVsParserObj.GetComments(PathToXmlDocumentation);
            list.Should().NotBeNull();
            list.Length.Should().BePositive();
            foreach (var comment in list)
            {
                comment.Remarks.Should().NotBeNull();
                comment.Returns.Should().NotBeNull();
                comment.Summary.Should().NotBeNull();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void XmlVsParser_WhenNullablePath_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => XmlVsParserObj.GetComments(null));
        }
    }
}
