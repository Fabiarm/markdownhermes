using System;
using System.IO;
using FluentAssertions;
using MarkDown.Hermes.Helper;
using MarkDown.Hermes.Interfaces;
using Moq;
using NUnit.Framework;

namespace UnitTest.MarkDown.Hermes.Helper
{
    [TestFixture]
    public class UnitTestValidator : UnitTestHermesBase
    {
        private Mock<IOutputWriter> _writerMock;
        public override bool UseOptions => true;
        public override void SetUpConfig()
        {
            _writerMock = new Mock<IOutputWriter>();
        }

        [Test]
        [Category("Integration")]
        public void Validator_Should_ReturnValidResult()
        {
            Validator.CheckOptions(OptionsObj, _writerMock.Object);
        }

        [Test]
        public void Validator_WhenNullableOptions_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => Validator.CheckOptions(null, _writerMock.Object));
        }

        [Test]
        public void Validator_GetXmlDocPath_WhenNullableParam_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => Validator.GetXmlDocPath(null));
        }

        [Test]
        [Category("Integration")]
        public void Validator_GetXmlDocPath_Should_ReturnValidResult()
        {
            var result = Validator.GetXmlDocPath(OptionsObj);
            result.Should().Be(XmlPathObj);
        }

        [Test]
        public void Validator_CheckDirectories_WhenNullableParam_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => Validator.CheckDirectories(null, _writerMock.Object));
        }

        [Test]
        public void Validator_CheckDirectories_WhenNullableOutputWriter_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => Validator.CheckDirectories(OptionsObj, null));
        }

        [Test]
        [Category("Integration")]
        public void Validator_CheckDirectories_Should_ReturnNoException()
        {
            Validator.CheckDirectories(OptionsObj, _writerMock.Object);
        }

        [Test]
        public void Validator_IsFileExist_WhenNullableParam_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => Validator.IsFileExist(null));
        }

        [Test]
        public void Validator_GetDllPath_WhenNullableParam_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => Validator.GetDllPath(null));
        }

        [Test]
        [Category("Integration")]
        public void Validator_GetDllPath_Should_ReturnValidResult()
        {
            var result = Validator.GetDllPath(OptionsObj);
            result.Should().Be(DllPathObj);
        }

        [Test]
        [Category("Integration")]
        public void Validator_CheckOptions_WhenInvalidInputDllFilePath_Should_ReturnException()
        {
            OptionsObj.InputDllFilePath = "test";

            Assert.Throws(typeof(FileNotFoundException),
                () => Validator.CheckOptions(OptionsObj, _writerMock.Object));
        }

        [Test]
        public void Validator_CheckOptions_WhenInvalidOutputWriter_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => Validator.CheckOptions(OptionsObj, null));
        }

        [Test]
        [Category("Integration")]
        public void Validator_CheckOptions_WhenEmptyOptions_Should_ReturnHelpUsage()
        {
            OptionsObj.InputDllFilePath = string.Empty;
            OptionsObj.OutputDirectoryPath = string.Empty;

            Validator.CheckOptions(OptionsObj, _writerMock.Object);
            _writerMock.Verify(s => s.WriteInfo(It.IsAny<string>()), Times.AtLeast(1));
        }

        [Test]
        public void Validator_CheckOptions_WhenNonExistInputDllFilePath_Should_ReturnException()
        {
            OptionsObj.InputDllFilePath = "C:\\test.dll";

            Assert.Throws(typeof(FileNotFoundException),
                () => Validator.CheckOptions(OptionsObj, _writerMock.Object));
        }

        [Test]
        public void Validator_WhenInvalidOutputWriter_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => Validator.CheckOptions(OptionsObj, null));
        }
    }
}