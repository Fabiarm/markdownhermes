using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkDown.Generator;
using MarkDown.Generator.Models;
using NUnit.Framework;

namespace UnitTest.MarkDown.Generator
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class UnitTestMdFluentBuilder: UnitTestMdBase
    {
        private MdFluentBuilder _builder;
        /// <inheritdoc />
        public override bool UseTypes => false;

        /// <inheritdoc />
        public override bool UseComments => false;

        /// <inheritdoc />
        public override bool UseMarkDownGenerator => true;

        /// <inheritdoc />
        public override bool UseXmlVsParser => true;

        /// <inheritdoc />
        public override bool UseTestExecutingAssembly => true;

        /// <inheritdoc />
        public override void SetUpConfig()
        {
            _builder = null;
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase("", "filename", true)]
        [TestCase("template", "", true)]
        [TestCase(" ", "filename", true)]
        [TestCase("template", " ", true)]
        [TestCase(null, "filename", true)]
        [TestCase("template", null, true)]
        [TestCase("template", "filename", false)]
        public void MdFluentBuilder_Build_Should_ReturnException(string template, string fileName, bool isType)
        {
            MarkDownType type = null;
            if (isType)
                type = new MarkDownType(MemberType.Property, null, false);
            Assert.Throws(typeof(ArgumentNullException),
                () => _builder = new MdFluentBuilder(template, fileName, type));
        }
    }
}
