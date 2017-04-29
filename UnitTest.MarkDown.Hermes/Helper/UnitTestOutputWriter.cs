using NUnit.Framework;
using MarkDown.Hermes.Helper;
using System;

namespace UnitTest.MarkDown.Hermes.Helper
{
    [TestFixture]
    public class UnitTestOutputWriter
    {
        private OutputWriter _writerObj;

        [SetUp]
        public void SetUp()
        {
            _writerObj = new OutputWriter();
        }

        [TestCase("text")]
        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public void OutputWriter_WriteInfo_Should_ReturnValidResult(string text)
        {
            _writerObj.WriteInfo(text);
        }

        [Test]
        public void OutputWriter_WriteError_WhenNullableException_Should_ReturnValidResult()
        {
            _writerObj.WriteError(null);
        }

        [Test]
        public void OutputWriter_WriteError_WhenThrowException_Should_ReturnValidResult()
        {
            var ex = new Exception("Test");
            _writerObj.WriteError(ex);
        }
    }
}
