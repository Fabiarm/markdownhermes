using System;
using FluentAssertions;
using MarkDown.Generator.Models;
using NUnit.Framework;

namespace UnitTest.MarkDown.Generator
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class UnitTestMdGenerator: UnitTestMdBase
    {
        /// <inheritdoc />
        public override bool UseTypes => true;

        /// <inheritdoc />
        public override bool UseComments => true;

        /// <inheritdoc />
        public override bool UseMarkDownGenerator => true;

        /// <inheritdoc />
        public override bool UseXmlVsParser => false;

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
        public void MdGenerator_GetTypes_Should_ReturnValidState()
        {
            var result = MarkDownGeneratorObj.GetTypes(GetExecutingAssembly);
            result.Should().NotBeNull();
            result.Count.Should().Be(3);
            foreach (var item in result)
            {
                item.Name.Should().NotBeNullOrEmpty();
                item.FullName.Should().NotBeNullOrEmpty();
                item.MemberType.Should().NotBeNull();
                item.Summary.Should().BeNullOrEmpty();
                item.ToString().Should().NotBeNullOrEmpty();

                foreach (var property in item.Properties)
                {
                    property.PropertyType.Should().NotBeNull();
                    property.Name.Should().NotBeNullOrEmpty();
                    property.FullName.Should().NotBeNullOrEmpty();
                    property.MemberType.Should().Be(MemberType.Property);
                    property.Summary.Should().BeNullOrEmpty();
                    property.ToString().Should().NotBeNullOrEmpty();
                }

                foreach (var method in item.Methods)
                {
                    method.ReturnParameter.Should().NotBeNull();
                    method.ReturnType.Should().NotBeNull();
                    method.Name.Should().NotBeNullOrEmpty();
                    method.FullName.Should().NotBeNullOrEmpty();
                    method.MemberType.Should().Be(MemberType.Method);
                    method.Summary.Should().BeNullOrEmpty();
                    method.ToString().Should().NotBeNullOrEmpty();
                }

                foreach (var field in item.Fields)
                {
                    field.PropertyType.Should().NotBeNull();
                    field.Name.Should().NotBeNullOrEmpty();
                    field.FullName.Should().NotBeNullOrEmpty();
                    field.MemberType.Should().Be(MemberType.Field);
                    field.Summary.Should().BeNullOrEmpty();
                    field.ToString().Should().NotBeNullOrEmpty();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdGenerator_GetTypes_Should_ReturnException()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => MarkDownGeneratorObj.GetTypes(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MdGenerator_UpdateComments_Should_ReturnValidState()
        {
            var result = MarkDownGeneratorObj.UpdateComments(Types, Comments);
            foreach (var item in result)
            {
                item.Summary.Should().NotBeNullOrEmpty();

                foreach (var property in item.Properties)
                    property.Summary.Should().NotBeNullOrEmpty();

                foreach (var method in item.Methods)
                    method.Summary.Should().NotBeNullOrEmpty();

                foreach (var field in item.Fields)
                    field.Summary.Should().NotBeNullOrEmpty();
            }
        }

    }
}
