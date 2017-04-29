using FluentAssertions;
using MarkDown.Generator.Helper;
using NUnit.Framework;

namespace UnitTest.MarkDown.Generator.Helper
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class UnitTestSpecHelper
    {
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void SpecHelper_CreateGenerator_Should_ReturnValidResult()
        {
            var result = SpecHelper.CreateGenerator();
            result.Should().NotBeNull();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void SpecHelper_CreateXmlVsParser_Should_ReturnValidResult()
        {
            var result = SpecHelper.CreateXmlVsParser();
            result.Should().NotBeNull();
        }
    }
}
