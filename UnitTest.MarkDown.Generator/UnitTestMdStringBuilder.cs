using FluentAssertions;
using MarkDown.Generator;
using NUnit.Framework;

namespace UnitTest.MarkDown.Generator
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class UnitTestMdStringBuilder
    {
        private MdStringEditor _editor;

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _editor = new MdStringEditor("fileName");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_Append_Should_ReturnValidResult()
        {
            _editor.Append("text");
            _editor.ToString().Should().Be("text");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_FileName_Should_ReturnValidResult()
        {
            _editor.FileName.Should().Be("fileName");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_AppendLine_Should_ReturnValidResult()
        {
            _editor.AppendLine("text01");
            _editor.AppendLine("text02");
            _editor.ToString().Should().Be("text01\r\ntext02\r\n");
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_Code_Should_ReturnValidResult()
        {
            _editor.Code("C#", "code");
            _editor.ToString().Should().Be("```C#\r\ncode\r\n```\r\n");
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_CodeQuote_Should_ReturnValidResult()
        {
            _editor.CodeQuote("code");
            _editor.ToString().Should().Be("`code`");
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_Header_Should_ReturnValidResult()
        {
            _editor.Header(1, "code");
            _editor.ToString().Should().Be("# code\r\n");
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_HeaderWithCode_Should_ReturnValidResult()
        {
            _editor.HeaderWithCode(1, "code");
            _editor.ToString().Should().Be("# `code`\r\n");
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_Image_Should_ReturnValidResult()
        {
            _editor.Image("alt", "http://www.yyyy.com");
            _editor.ToString().Should().Be("![alt](http://www.yyyy.com)");
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_Link_Should_ReturnValidResult()
        {
            _editor.Link("alt", "http://www.yyyy.com");
            _editor.ToString().Should().Be("[alt](http://www.yyyy.com)");
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_List_Should_ReturnValidResult()
        {
            _editor.List("alt");
            _editor.ToString().Should().Be("- alt\r\n");
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdStringEditor_ListLink_Should_ReturnValidResult()
        {
            _editor.ListLink("alt", "http://www.yyyy.com");
            _editor.ToString().Should().Be("- [alt](http://www.yyyy.com)\r\n");
        }
       
    }
}
