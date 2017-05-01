using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MarkDown.Generator;
using MarkDown.Generator.Interfaces;
using MarkDown.Generator.Models;
using NUnit.Framework;
using UnitTest.MarkDown.Generator.Helper;

namespace UnitTest.MarkDown.Generator
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class UnitTestMdFluentBuilder: UnitTestMdBase
    {
        private MdFluentBuilder _builder;
        private MarkDownBuilder _mdBuilder;
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdFluentBuilder_Build_Should_ReturnValidContentForParent()
        {
            _mdBuilder = new MarkDownBuilder(MarkDownGeneratorObj, XmlVsParserObj);
            _mdBuilder.Load(PathToDll, PathToXmlDocumentation);
            var type = _mdBuilder.Types.First(s => s.MemberType == MemberType.Type);
            _builder = new MdFluentBuilder(TestVariables.TemplateHeader, "home.md", type);
            var result = _builder.Build();
            result.Should().NotBeNull();
            result.Should().BeOfType<MdStringEditor>();
            result.ToString().Should().NotBeNullOrEmpty();
            result.ToString().Should().NotContain("@prefix");
            result.ToString().Should().NotContain("@fullName");
            result.ToString().Should().NotContain("@summary");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdFluentBuilder_Build_Should_ReturnValidContentForProperties()
        {
            _mdBuilder = new MarkDownBuilder(MarkDownGeneratorObj, XmlVsParserObj);
            _mdBuilder.Load(PathToDll, PathToXmlDocumentation);
            var type = _mdBuilder.Types.First(s => s.MemberType == MemberType.Type && s.Properties.Count > 0);
            _builder = new MdFluentBuilder(TestVariables.TemplateProperties, "home.md", type);
            var result = _builder.Build();
            CheckResult(result, "prop");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdFluentBuilder_Build_Should_ReturnValidContentForFields()
        {
            _mdBuilder = new MarkDownBuilder(MarkDownGeneratorObj, XmlVsParserObj);
            _mdBuilder.Load(PathToDll, PathToXmlDocumentation);
            var type = _mdBuilder.Types.First(s => s.MemberType == MemberType.Type && s.Fields.Count > 0);
            _builder = new MdFluentBuilder(TestVariables.TemplateFields, "home.md", type);
            var result = _builder.Build();
            CheckResult(result, "field");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdFluentBuilder_Build_Should_ReturnValidContentForMethods()
        {
            _mdBuilder = new MarkDownBuilder(MarkDownGeneratorObj, XmlVsParserObj);
            _mdBuilder.Load(PathToDll, PathToXmlDocumentation);
            var type = _mdBuilder.Types.First(s => s.MemberType == MemberType.Type && s.Methods.Count > 0);
            _builder = new MdFluentBuilder(TestVariables.TemplateMethods, "home.md", type);
            var result = _builder.Build();
            CheckResult(result, "method");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdFluentBuilder_Build_Should_ReturnValidFullContent()
        {
            _mdBuilder = new MarkDownBuilder(MarkDownGeneratorObj, XmlVsParserObj);
            _mdBuilder.Load(PathToDll, PathToXmlDocumentation);

            var list = new List<IMdStringEditor>();

            foreach (var type in _mdBuilder.Types)
            {
                _builder = new MdFluentBuilder(TestVariables.TemplateFull, "home.md", type);
                var result = _builder.Build();
                result.Should().NotBeNull();
                result.Should().BeOfType<MdStringEditor>();
                result.ToString().Should().NotBeNullOrEmpty();
                result.ToString().Should().NotContain("@");
                list.Add(result);
            }
            list.Count.Should().Be(_mdBuilder.Types.Count);
        }

        private void CheckResult(IMdStringEditor result, string tag)
        {
            result.Should().NotBeNull();
            result.Should().BeOfType<MdStringEditor>();
            result.ToString().Should().NotBeNullOrEmpty();
            result.ToString().Should().NotContain($"@{tag}.type");
            result.ToString().Should().NotContain($"@{tag}.name");
            result.ToString().Should().NotContain($"@{tag}.summary");
            result.ToString().Should().NotContain($"@{tag}s");
            result.ToString().Should().NotContain($"@end{tag}s");
            result.ToString().Should().NotContain($"@{tag}");
            result.ToString().Should().NotContain($"@end{tag}");
        }
    }
}
