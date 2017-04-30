using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MarkDown.Generator;
using MarkDown.Generator.Interfaces;
using MarkDown.Generator.Models;
using Moq;
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
        /// <param name="template"></param>
        [TestCase(@"
        ### @prefix
        __Namespace__: @fullName
        * * *
        __Summary__: @summary
        }
        ")]
        public void MdFluentBuilder_Build_Should_ReturnValidContentForParent(string template)
        {
            _mdBuilder = new MarkDownBuilder(MarkDownGeneratorObj, XmlVsParserObj);
            _mdBuilder.Load(PathToDll, PathToXmlDocumentation);
            var type = _mdBuilder.Types.First(s => s.MemberType == MemberType.Type);
            _builder = new MdFluentBuilder(template, "home.md", type);
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
        /// <param name="template"></param>
        [TestCase(@"
        @@properties{
        * * *
        __Properties__
        
        | Value | Name | Summary |
        | --- | --- | --- |
        | @prop.type | @prop.name | @prop.summary |
        
        * * *
        }
        ")]
        [Ignore("")]
        public void MdFluentBuilder_Build_Should_ReturnValidContentForProperties(string template)
        {
            _mdBuilder = new MarkDownBuilder(MarkDownGeneratorObj, XmlVsParserObj);
            _mdBuilder.Load(PathToDll, PathToXmlDocumentation);
            var type = _mdBuilder.Types.First(s => s.MemberType == MemberType.Type && s.Properties.Count > 0);
            _builder = new MdFluentBuilder(template, "home.md", type);
            var result = _builder.Build();
            result.Should().NotBeNull();
            result.Should().BeOfType<MdStringEditor>();
            result.ToString().Should().NotBeNullOrEmpty();
            result.ToString().Should().NotContain("@prop.type");
            result.ToString().Should().NotContain("@prop.name");
            result.ToString().Should().NotContain("@prop.summary");
            result.ToString().Should().NotContain("@@properties{");
            result.ToString().Should().NotContain("}");
        }

        private Mock<MarkDownField> GetFieldMock(int id, string parentFullName)
        {
            var field = new Mock<MarkDownField>(MemberType.Field, null);
            field.Setup(s => s.Name).Returns($"Field0{id}");
            field.Setup(s => s.FullName).Returns(parentFullName);
            field.Setup(s => s.Prefix).Returns($"public field Field0{id}");
            field.Setup(s => s.Summary).Returns($"summary Field0{id}");
            return field;
        }

        private Mock<MarkDownMethod> GetMethodMock(int id, string parentFullName)
        {
            var method = new Mock<MarkDownMethod>(MemberType.Method, null);
            method.Setup(s => s.Name).Returns($"Method0{id}");
            method.Setup(s => s.FullName).Returns(parentFullName);
            method.Setup(s => s.Prefix).Returns($"public void Method0{id}");
            method.Setup(s => s.Summary).Returns($"summary Method0{id}");
            return method;
        }

        private Mock<MarkDownProperty> GetPropertyMock(int id, string parentFullName)
        {
            var prop = new Mock<MarkDownProperty>(MemberType.Property, null);
            prop.Setup(s => s.Name).Returns($"Prop0{id}");
            prop.Setup(s => s.FullName).Returns(parentFullName);
            prop.Setup(s => s.PropertyType).Returns(typeof(int));
            prop.Setup(s => s.Prefix).Returns($"public strict Prop0{id}");
            prop.Setup(s => s.Summary).Returns($"summary Prop0{id}");
            return prop;
        }
    }
}
