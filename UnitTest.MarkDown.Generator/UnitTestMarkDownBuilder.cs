using System;
using FluentAssertions;
using MarkDown.Generator;
using MarkDown.Generator.Interfaces;
using NUnit.Framework;

namespace UnitTest.MarkDown.Generator
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class UnitTestMarkDownBuilder : UnitTestMdBase
    {
        private MarkDownBuilder _builder;
        /// <inheritdoc />
        public override bool UseTypes => true;

        /// <inheritdoc />
        public override bool UseComments => true;

        /// <inheritdoc />
        public override bool UseMarkDownGenerator => true;

        /// <inheritdoc />
        public override bool UseXmlVsParser => true;

        /// <inheritdoc />
        public override bool UseTestExecutingAssembly => true;

        /// <inheritdoc />
        public override void SetUpConfig()
        {
            _builder = new MarkDownBuilder(MarkDownGeneratorObj, XmlVsParserObj);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MarkDownBuilder_Load_Should_ReturnNoException()
        {
            _builder.Load(PathToDll, PathToXmlDocumentation);
            _builder.Types.Should().NotBeNull();
            _builder.Types.Count.Should().BePositive();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MarkDownBuilder_Build_Should_ReturnNoException()
        {
            _builder.Load(PathToDll, PathToXmlDocumentation);
            _builder.Types.Should().NotBeNull();
            _builder.Types.Count.Should().BePositive();
            _builder.Build();
            _builder.Content.Should().NotBeNull();
            _builder.Content.Should().AllBeOfType<MdStringEditor>();
            _builder.Content.Should().AllBeAssignableTo<IMdStringEditor>();
            foreach (var stringBuilder in _builder.Content)
                stringBuilder.ToString().Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MarkDownBuilder_Load_WhenInvalidDllPath_Should_ReturnException()
        {

            Assert.Throws(typeof(ArgumentNullException),
                () => _builder.Load(null, "null"));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MarkDownBuilder_Build_Should_ReturnException()
        {
            Assert.Throws(typeof(OperationCanceledException),
                () => _builder.Build());
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MarkDownBuilder_BuildWithTemplate_Should_ReturnException()
        {
            _builder.Load(PathToDll, PathToXmlDocumentation);
            _builder.Types.Should().NotBeNull();
            _builder.Types.Count.Should().BePositive();
            Assert.Throws(typeof(ArgumentNullException),
                () => _builder.Build(" "));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MarkDownBuilder_BuildWithTemplate_WhenNullableTypes_Should_ReturnException()
        {

            Assert.Throws(typeof(OperationCanceledException),
                () => _builder.Build("template"));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCase(@"
        ### @Prefix
        __Namespace__: @FullName
        * * *
        __Summary__: @Summary

        if(@properties){
        * * *
        __Properties__
        }
        ")]
        public void MarkDownBuilder_BuildWithTemplate_Should_ReturnNoException(string template)
        {
            _builder.Load(PathToDll, PathToXmlDocumentation);
            _builder.Types.Should().NotBeNull();
            _builder.Types.Count.Should().BePositive();
            _builder.Build(template);
            _builder.Content.Should().NotBeNull();
            _builder.Content.Should().AllBeOfType<MdStringEditor>();
            _builder.Content.Should().AllBeAssignableTo<IMdStringEditor>();
            foreach (var stringBuilder in _builder.Content)
                stringBuilder.ToString().Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MarkDownBuilder_Load_WhenInvalidXmlDllPath_Should_ReturnException()
        {

            Assert.Throws(typeof(ArgumentNullException),
                () => _builder.Load("null", null));
        }
    }
}
