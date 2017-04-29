using System;
using MarkDown.Hermes.Helper;
using MarkDown.Hermes.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTest.MarkDown.Hermes.Helper
{
    [TestFixture]
    public class UnitTestExecutor : UnitTestHermesBase
    {
        private string[] _args;
        private Mock<IOutputWriter> _writerMock;
        public override bool UseOptions => true;

        public override void SetUpConfig()
        {
            _args = new[] {"-d", DllPathObj, "-o", OutPutPathObj};
            _writerMock = new Mock<IOutputWriter>();
        }

        [TestCase("path", "")]
        [TestCase("", "path")]
        [TestCase("path", " ")]
        [TestCase(" ", "path")]
        public void Program_WhenInCorrectParams_Should_ReturnVoid(string dllPathObj, string outPutPathObj)
        {
            _args = new[] {"-d", dllPathObj, "-o", outPutPathObj};
            Executor.Run(_args, _writerMock.Object);
            _writerMock.Verify(s => s.WriteError(It.IsAny<Exception>()), Times.AtLeast(1));
        }

        [Test]
        public void Program_WhenInvalidParams_Should_ReturnVoid()
        {
            _args = new[] { "-n", DllPathObj, "-m", OutPutPathObj };
            Executor.Run(_args, _writerMock.Object);
            _writerMock.Verify(s => s.WriteError(It.IsAny<Exception>()), Times.AtLeast(1));
        }

        [Test]
        [Category("Integration")]
        public void Program_WhenCorrectParams_Should_ReturnValidState()
        {
            Executor.Run(_args, _writerMock.Object);
            _writerMock.Verify(s => s.WriteInfo(It.IsAny<string>()), Times.AtLeast(1));
        }
    }
}
