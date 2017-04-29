using NUnit.Framework;
using System;
using MarkDown.Generator.Exceptions;

namespace UnitTest.MarkDown.Generator.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class UnitTestConditionValidator
    {
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConditionValidator_Should_ReturnArgumentOutOfRangeException()
        {
            //act
            Assert.Throws(typeof(ArgumentOutOfRangeException),
                () => ConditionValidator.ThrowExceptionIfNotValid<ArgumentOutOfRangeException>((true), "name"));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConditionValidator_WhenFalseCondition_Should_ReturnArgumentOutOfRangeException()
        {
            //act
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentOutOfRangeException>((false), "name");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConditionValidator_Should_ReturnArgumentOutOfRangeException_With_Message()
        {
            //act
            Assert.Throws(typeof(ArgumentOutOfRangeException),
                () => ConditionValidator.ThrowExceptionIfNotValid<ArgumentOutOfRangeException>((true), "name"), "string");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConditionValidator_WhenFalseCondition_Should_ReturnArgumentOutOfRangeException_With_Message()
        {
            //act
            ConditionValidator.ThrowExceptionIfNotValid<ArgumentOutOfRangeException>((false), "name", "string");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConditionValidator_Should_ReturnArgumentNullException()
        {
            //act
            Assert.Throws(typeof(ArgumentNullException),
                () => ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((true), "name"));
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConditionValidator_Should_ReturnArgumentNullException_With_Message()
        {
            //act
            Assert.Throws(typeof(ArgumentNullException),
                () => ConditionValidator.ThrowExceptionIfNotValid<ArgumentNullException>((true), "name", "string"));
        }
    }
}
